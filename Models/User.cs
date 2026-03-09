using System.ComponentModel.DataAnnotations;

namespace CreditCardAppMvc.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        public string PasswordHash { get; set; } = "";

        public string Role { get; set; } = "User";

        public List<Application>? Applications { get; set; }
    }
}