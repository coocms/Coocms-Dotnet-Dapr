using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet_Dapr_Demo.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return Ok("This is Service");
        }
    }
}
