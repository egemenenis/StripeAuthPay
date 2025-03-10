using Stripe;
using PaymentIntegrationProject.Models;
using System.Threading.Tasks;

namespace PaymentIntegrationProject.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _stripeSecretKey;

        public PaymentService()
        {
            _stripeSecretKey = "sk_test_SecretKey";
            StripeConfiguration.ApiKey = _stripeSecretKey;
        }

        public async Task<PaymentResult> ProcessPayment(PaymentModel payment)
        {
            var paymentResult = new PaymentResult();

            try
            {
                var paymentIntentService = new PaymentIntentService();
                var paymentIntentCreateOptions = new PaymentIntentCreateOptions
                {
                    Amount = (long)(payment.Amount * 100),
                    Currency = "usd",
                    PaymentMethod = payment.PaymentMethodId,
                    Confirm = true
                };

                var paymentIntent = await paymentIntentService.CreateAsync(paymentIntentCreateOptions);

                paymentResult.PaymentSuccessful = true;
                paymentResult.Message = "Payment completed successfully.";
                paymentResult.PaymentIntentId = paymentIntent.Id;
            }
            catch (StripeException ex)
            {
                paymentResult.PaymentSuccessful = false;
                paymentResult.Message = $"Stripe Error: {ex.Message}";
            }
            catch (System.Exception ex)
            {
                paymentResult.PaymentSuccessful = false;
                paymentResult.Message = $"An unexpected error occurred: {ex.Message}";
            }

            return paymentResult;
        }
    }
}
