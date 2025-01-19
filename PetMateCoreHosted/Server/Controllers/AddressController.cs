using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetMateCoreHosted.Server.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetMateCoreHosted.Server.Controllers
{
    // Define the api endpoint for controller
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // Private field hold instance of the DynamoDB table
        private readonly IDynamoDBContext _context;

        // Constructor for AddressController, takes an IDynamoDBContext instance as a parameter
        // to be injected into the controller
        public AddressController(IDynamoDBContext context)
        {
            // Assigns the injected context to the private field
            _context = context;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            // Loading a Address asynchronously from the DynamoDB table based on provided id and barcode
            var address = await _context.LoadAsync<Address>(username);
            // If the Address is not found, return NotFound if true else return the Address
            if (address == null) return NotFound();
            return Ok(address);
        }

        // HTTP GET method to retrieve ALL Address
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Returns all Address asynchronously from the DynamoDB table
            var addresses = await _context.ScanAsync<Address>(default).GetRemainingAsync();
            // Returns the retrieved Address
            return Ok(addresses);
        }

        // HTTP POST method to create a new Address
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(Address request)
        {
            // Loads a Address asynchronously from the DynamoDB table based on provided id and barcode
            var address = await _context.LoadAsync<Address>(request.Username);
            // Checking if Address with same id already exists, returns BadRequest if true
            if (address != null)
            {
                await _context.DeleteAsync(address);
            }

            await _context.SaveAsync(request);
            // Returning the created Address
            return Ok(request);
        }

        // HTTP DELETE method to delete a Address by its id and barcode
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            // Loads a Address asynchronously from the DynamoDB table based on provided id and barcode
            var address = await _context.LoadAsync<Address>(username);
            // Checking if the Address is not found and returning NotFound if true
            if (address == null) return NotFound();
            // asynchronously deletes from the DynamoDB table
            await _context.DeleteAsync(address);
            // Returns NoContent to indicate successful deletion
            return NoContent();
        }

        // HTTP PUT method to update an existing Address
        [HttpPut]
        public async Task<IActionResult> Update(Address request)
        {
            // Loads a Address asynchronously from the DynamoDB table based on provided id and barcode
            var address = await _context.LoadAsync<Address>(request.Username);
            // Checking if the Address is not found and returning NotFound if true
            if (address == null) return NotFound();
            // Saving the updated Address asynchronously to the DynamoDB table
            await _context.SaveAsync(request);
            // Returning the updated Address
            return Ok(request);
        }
    }
}
