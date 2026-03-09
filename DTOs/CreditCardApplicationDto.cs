using System.ComponentModel.DataAnnotations;

namespace CreditCardAppMvc.DTOs
{
    public class CreditCardApplicationDto
    {
        [Required]
        [MaxLength(10)]
        public string PAN { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public decimal AnnualIncome { get; set; }

        [Required]
        public string Name { get; set; }
    }
}