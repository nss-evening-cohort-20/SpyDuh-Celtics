using System.ComponentModel.DataAnnotations;

namespace SpyDuh_Celtics.Models
{
    public class Skills
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Skill { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class SkillInsert
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Skill { get; set; }
        public int UserId { get; set; }
    }
}
