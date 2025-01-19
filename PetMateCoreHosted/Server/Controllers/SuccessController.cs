using System;
using Microsoft.AspNetCore.Mvc;

namespace PetMateCoreHosted.Server.Controllers
{
    [Route("success")]
    [ApiController]
    public class SuccessController : Controller
    {
        [HttpGet]  // Ensure this is a GET request since the redirection after payment will be a GET
        public IActionResult Index()
        {
            // Redirect to the client application's success page
            return Redirect("https://localhost:7128/success-page");
        }
    }
}

