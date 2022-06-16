using AuthModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class Context : DbContext
    {
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        // public Context() => DbPath = "server=localhost;user=root;database=praunote;password=;";
        public Context() => DbPath = "praunote.db";

        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql(DbPath, new MySqlServerVersion(new Version(5, 7, 36)));
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
