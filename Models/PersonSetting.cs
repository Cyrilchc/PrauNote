using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class PersonSetting
    {
        [Key]
        public int Id { get; set; }
        public string Font { get; set; }
        public double FontSize { get; set; }
        public string Language { get; set; }
        public string Theme { get; set; }
        public int PersonId { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

    }
}
