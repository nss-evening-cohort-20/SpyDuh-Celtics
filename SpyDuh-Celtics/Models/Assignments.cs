using System.ComponentModel.DataAnnotations;

namespace SpyDuh_Celtics.Models
{
    public class Assignments
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
    }

}
