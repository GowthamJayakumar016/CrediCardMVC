using System.ComponentModel.DataAnnotations;

namespace CreditCardAppMvc.Models
{
    public class Application
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(10)]
        public string PAN { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public decimal AnnualIncome { get; set; }

        public int CreditScore { get; set; }

        public decimal CreditLimit { get; set; }

        public string Status { get; set; }

        public User User { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}