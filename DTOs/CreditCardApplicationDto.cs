using System.ComponentModel.DataAnnotations;

namespace CreditCardAppMvc.DTOs
{
    public class CreditCardApplicationDto
    {
        [Required]
        public string PAN { get; set; } = "";

        public DateTime DOB { get; set; }

        public decimal AnnualIncome { get; set; }

        public string Name { get; set; } = "";
    }
}