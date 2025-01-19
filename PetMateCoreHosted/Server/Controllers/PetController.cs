using System;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetMateCoreHosted.Server.Models;

namespace PetMateCoreHosted.Server.Controllers
{
    // Define the api endpoint for controller
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        // Private field hold instance of the DynamoDB table
        private readonly IDynamoDBContext _context;

        // Constructor for PetsController, takes an IDynamoDBContext instance as a parameter
        // to be injected into the controller
        public PetController(IDynamoDBContext context)
        {
            // Assigns the injected context to the private field
            _context = context;
        }

        [HttpGet("{id}/{name}")]
        public async Task<IActionResult> Get(string id, string name)
        {
            // Loading a Pet asynchronously from the DynamoDB table based on provided id and barcode
            var pet = await _context.LoadAsync<Pet>(id, name);
            // If the Pet is not found, return NotFound if true else return the Pet
            if (pet == null) return NotFound();
            return Ok(pet);
        }


        // HTTP GET method to retrieve ALL Pets
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Returns all Pets asynchronously from the DynamoDB table
            var pets = await _context.ScanAsync<Pet>(default).GetRemainingAsync();
            // Returns the retrieved Pets
            return Ok(pets);
        }

        // HTTP POST method to create a new Pet
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(Pet request)
        {
            // Loads a Pet asynchronously from the DynamoDB table based on provided id and barcode
            var pet = await _context.LoadAsync<Pet>(request.Id, request.Username);
            // Checking if Pet with same id already exists, returns BadRequest if true
            if (pet != null) return BadRequest($"Pet with Id {request.Id} and BarCode {request.Name} Already Exists");

            await _context.SaveAsync(request);
            // Returning the created Pet
            return Ok(request);
        }

        // HTTP DELETE method to delete a Pet by its id and barcode
        [HttpDelete("{id}/{name}")]
        public async Task<IActionResult> Delete(string id, string name)
        {
            // Loads a Pet asynchronously from the DynamoDB table based on provided id and barcode
            var pet = await _context.LoadAsync<Pet>(id, name);
            // Checking if the Pet is not found and returning NotFound if true
            if (pet == null) return NotFound();
            // asynchronously deletes from the DynamoDB table
            await _context.DeleteAsync(pet);
            // Returns NoContent to indicate successful deletion
            return NoContent();
        }

        // HTTP PUT method to update an existing Pet
        [HttpPut]
        public async Task<IActionResult> Update(Pet request)
        {
            // Loads a Pet asynchronously from the DynamoDB table based on provided id and barcode
            var pet = await _context.LoadAsync<Pet>(request.Id, request.Username);
            // Checking if the Pet is not found and returning NotFound if true
            if (pet == null) return NotFound();
            // Saving the updated Pet asynchronously to the DynamoDB table
            await _context.SaveAsync(request);
            // Returning the updated Pet
            return Ok(request);
        }
    }
}
