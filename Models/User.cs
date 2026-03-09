using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace CreditCardAppMvc.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public List<Application> Applications { get; set; }
    }
}