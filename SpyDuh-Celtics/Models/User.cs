
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SpyDuh_Celtics.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
