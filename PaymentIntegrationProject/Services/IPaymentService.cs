using PaymentIntegrationProject.Models;
using System.Threading.Tasks;

namespace PaymentIntegrationProject.Services
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPayment(PaymentModel payment);
    }
}
