using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;

namespace Subscriber.Controllers
{
    [ApiController]
    public class CheckoutServiceController : Controller
    {
        [Topic("order-pub-sub", "orders")]
        [HttpPost("checkout")]

        public void getCheckout([FromBody] int orderId)
        {
            Console.WriteLine("Subscriber received : " + orderId);
        }
    }
}
