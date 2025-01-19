using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace PetMateCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleMapsController : ControllerBase
    {
        [EnableCors("MyPolicy")]
        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> GetAll(string searchTerm)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={searchTerm}&types=geocode&key=AIzaSyD4ozdOieSzUwW4f0S1b8X6pn9QDdGqw7I");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                Console.WriteLine("addresses have been called");
                return Ok(content); // Return the content as OK result
            }
            else
            {
                return StatusCode((int)response.StatusCode); // Return status code if request failed
            }
        }
    }
}

