using System.ComponentModel.DataAnnotations;

namespace ConnectionCheckerLibrary.DataBase.Models
{
    /// <summary>
    /// The connection.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [Key]
        [Url]
        [Required]
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the check delay.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [Required]
        public float CheckDelay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is on.
        /// </summary>
        public bool IsOn { get; set; }
    }
}
