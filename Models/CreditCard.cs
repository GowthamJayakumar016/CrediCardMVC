namespace CreditCardAppMvc.Models
{
    public class CreditCard
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public string CardNumber { get; set; } = "";

        public string DispatchNumber { get; set; } = "";

        public DateTime IssuedDate { get; set; }

        public Application? Application { get; set; }
    }
}