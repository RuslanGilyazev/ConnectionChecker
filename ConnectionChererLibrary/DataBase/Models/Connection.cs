using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionCheckerLibrary.DataBase.Models
{
    public class Connection
    {
        [Key]
        [Url]
        [Required]
        public string URL { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [Required]
        public float CheckDelay { get; set; }

        public bool IsOn { get; set; }
    }
}
