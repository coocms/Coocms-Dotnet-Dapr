using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using System.Text.Json.Serialization;

namespace Subscriber.Controllers
{
    [ApiController]
    public class CheckoutServiceController : Controller
    {
        [Topic("order-pub-sub", "orders")]
        [HttpPost("checkout")]

        public void getCheckout(Order order)
        {
            Console.WriteLine("Subscriber received : " + order);
        }
    }
    public record Order([property: JsonPropertyName("orderId")] int OrderId);
}
