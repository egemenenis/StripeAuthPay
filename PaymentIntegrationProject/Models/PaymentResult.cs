namespace PaymentIntegrationProject.Models
{
    public class PaymentResult
    {
        public bool PaymentSuccessful { get; set; }
        public string Message { get; set; }
        public string PaymentIntentId { get; set; }
        public string PaymentStatus { get; set; }
    }

}
