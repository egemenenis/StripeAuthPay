using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentIntegrationProject.Models;
using PaymentIntegrationProject.Services;
using Stripe;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly string _stripeSecretKey = "sk_test_SecretKey";

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
        StripeConfiguration.ApiKey = _stripeSecretKey;
    }

    [HttpPost("make-payment")]
    [Authorize]
    public IActionResult MakePayment([FromBody] PaymentModel payment)
    {
        if (payment == null)
        {
            return BadRequest(new { message = "Payment information is invalid." });
        }

        try
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntentCreateOptions = new PaymentIntentCreateOptions
            {
                Amount = (long)(payment.Amount * 100),
                Currency = payment.Currency ?? "usd",
                PaymentMethod = payment.PaymentMethodId,
                Confirm = true
            };

            var paymentIntent = paymentIntentService.Create(paymentIntentCreateOptions);

            return Ok(new PaymentResult
            {
                PaymentSuccessful = true,
                Message = "Payment successful.",
                PaymentIntentId = paymentIntent.Id,
                PaymentStatus = paymentIntent.Status
            });
        }
        catch (StripeException ex)
        {
            return BadRequest(new PaymentResult
            {
                PaymentSuccessful = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new PaymentResult
            {
                PaymentSuccessful = false,
                Message = "An unexpected error occurred.",
                PaymentStatus = "error"
            });
        }
    }
}
