using System.ComponentModel.DataAnnotations;

namespace mongomvc.Models
{
    public class User
    {
        public string Name { get; set; }
        [Required]
        [EmailAddress (ErrorMessage = "هرکس هست سلام")]
        public string Email { get; set; }
        [Required]
        
        public string Password { get; set; }
    }
}
