using Auth;
using Auth.Models;
using AuthModels;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config, Context context)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] SignInModel login)
        {
            IActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });

                // insère le jeton en base de données.
                await _context.AddAsync(new AuthToken() { Token = tokenString });
                await _context.SaveChangesAsync();
            }

            return response;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SignUpModel user)
        {
            #region Vérifie si l'objet est valide

            if (string.IsNullOrWhiteSpace(user.Mail))
                return BadRequest("L'email n'est pas renseigné");
            if (string.IsNullOrWhiteSpace(user.Password))
                return BadRequest("Le mot de passe n'est pas renseigné");
            if (!IsValidEmail(user.Mail))
                return BadRequest("L'email inséré est invalide");
            if (!IsValidPassword(user.Password))
                return BadRequest("Le mot de passe ne respecte pas les critères de sécurité");

            #endregion

            // Vérifie si ce mail est déjà utilisé
            if (_context.Users.Any(x => x.Email == user.Mail))
                return Conflict("Cette adresse mail est déjà utilisée");

            // Hash le mot de passe
            HashedPasswordModel hashedPasswordModel = PasswordHasher.HashPassword(user.Password);

            // Créé l'objet utilisateur
            User newUser = new User
            {
                Email = user.Mail,
                Password = hashedPasswordModel.HashedPassword,
                Salt = hashedPasswordModel.Salt
            };

            // Insère en base
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Renvoie no content
            return NoContent();
        }

        private string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Aud, _config["Api:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:5001"),
                new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:5002"),
                new Claim(JwtRegisteredClaimNames.Aud, "http://localhost:500etc..."),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Api:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // Audience simple
            // var token = new JwtSecurityToken(
            //     _config["Api:Issuer"],
            //     _config["Api:Issuer"],
            //     claims,
            //     expires: DateTime.Now.AddMinutes(30),
            //     signingCredentials: creds);

            // Audiences multiples : 
            // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/39#issuecomment-267233556
            // On ne met pas l'audience dans le contructeur (null ici) mais dans les claims avec la clé aud. (Voir les claims ci-dessus)
            var token = new JwtSecurityToken(
                _config["Api:Issuer"],
                null,
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Vérifier en base le secret et si trouvé, renvoyer l'utilisateur
        /// </summary>
        /// <param name="login"></param>
        /// <returns>L'utilisateur trouvé en base</returns>
        private async Task<User>Authenticate(SignInModel login)
        {
            // Récupère l'utilsateur en base qui correspond aux identifiants fournis
            User user = _context.Users.Where(x => x.Email == login.Mail).First();
            if (user != null)
            {
                // Un utilisateur avec ce mail existe. On vérifie maintenant si le mot de passe est correct.
                if (user.Password != PasswordHasher.HashPasswordWithSalt(login.Password, user.Salt)
                    .HashedPassword)
                    return null;
            }

            // Renvoie null si l'utilisateur n'est pas trouvé avec ce mail ou si le mot de passe ne correspond pas
            return user;
        }

        /// <summary>
        /// Vérifie si le mot de passe en paramètre est valide
        /// Difficulté : Au moins 1 chiffre, Au moins 1 majuscule, Au moins 8 caractères
        /// Source : https://stackoverflow.com/a/34715592/10506880
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsValidPassword(string password)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMinimum8Chars = new Regex(@".{8,}");
            return hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
        }

        /// <summary>
        /// Vérifie si le mail en paramètre est valide.
        /// Source : https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
