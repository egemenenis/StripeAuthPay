namespace PaymentIntegrationProject.Models
{
    public class PaymentModel
    {
        public decimal Amount { get; set; }
        public string PaymentMethodId { get; set; }
        public string Currency { get; set; }
    }

}
