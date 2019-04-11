using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BraintreeHttp;
using PayPal.Core;
using PayPal.v1.Payments;

namespace PizzaApp.Services
{
    public class PaypalService
    {
        public async Task<string> PayPalPaymentAsync(int orderPrice)
        {
            const string CLIENT_ID = "Ab8zPbxBbiYu1TuE-3jyw0M6m41tBR4mhjXNDP1ZzQMEJ4JfQ9yJJy3qLT2LrO0oIrf-qpm7beMLAAXD";
            const string CLIENT_SECRET = "EMSYp9szDMBOMFd-nag3_VbHHm7LieAVIFK4FZNtidyRfGpIQwhcnEfdwPlophxERxgkhHsUMRngbm09";

            var environment = new SandboxEnvironment(CLIENT_ID, CLIENT_SECRET);
            var client = new PayPalHttpClient(environment);

            var payment = new Payment
            {
                Intent = "order",

                Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        Amount = new Amount()
                        {
                            Total = orderPrice.ToString(),
                            Currency = "USD"
                        }

                    }
                },
                RedirectUrls = new RedirectUrls()
                {

                    CancelUrl = "https://example.com/cancel",
                    ReturnUrl = "https://example.com/return"
                },

                Payer = new Payer()
                {
                    PaymentMethod = "paypal"
                }
            };

            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);

            try
            {
                HttpResponse response = await client.Execute(request);
                var statusCode = response.StatusCode;
                Payment result = response.Result<Payment>();
                return response.StatusCode.ToString();
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
                return statusCode.ToString();
            }
        
        }
    }
}
