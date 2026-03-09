using System.ComponentModel.DataAnnotations;

namespace CreditCardAppMvc.Models
{
    public class Application
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string PAN { get; set; } = "";

        public DateTime DOB { get; set; }

        public decimal AnnualIncome { get; set; }

        public int CreditScore { get; set; }

        public decimal CreditLimit { get; set; }

        public string Status { get; set; } = "Pending";

        public User? User { get; set; }

        public CreditCard? CreditCard { get; set; }
    }
}