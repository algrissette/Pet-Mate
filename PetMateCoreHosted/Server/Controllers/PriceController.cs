using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetMateCoreHosted.Server.Models;
using Stripe;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetMateCoreHosted.Server.Controllers
{
    [Route("api/price")]
    public class PriceController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [EnableCors("MyPolicy")]
        [HttpPost]
        public ActionResult Post([FromBody] PriceCreateRequest request)
        {
            Console.WriteLine($"Request: {request}, Name: {request.Name}, Price: {request.Price}");

            if (request == null)
            {
                return BadRequest("Invalid data");
            }

            var options = new PriceCreateOptions
            {
                Currency = "usd",
                UnitAmount = request.Price * 100,
                ProductData = new PriceProductDataOptions { Name = request.Name },
            };

            try
            {
                var service = new PriceService();
                var price = service.Create(options);
                string id = price.Id;

                // Check if the price object is not null and has an ID
                if (price != null && !string.IsNullOrEmpty(price.Id))
                {
                    // Return the price ID
                    return Ok(new { PriceId = price.Id });
                }
                else
                {
                    return StatusCode(500, "Failed to create price");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

