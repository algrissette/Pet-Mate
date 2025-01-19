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
    public class ProductsController : ControllerBase
    {
        // Private field hold instance of the DynamoDB table
        private readonly IDynamoDBContext _context;

        // Constructor for ProductsController, takes an IDynamoDBContext instance as a parameter
        // to be injected into the controller
        public ProductsController(IDynamoDBContext context)
        {
            // Assigns the injected context to the private field
            _context = context;
        }

        // HTTP GET method to retrieve a specific product by its id and barcode
        [HttpGet("{id}/{barcode}")]
        public async Task<IActionResult> Get(string id, string barcode)
        {
            // Loading a product asynchronously from the DynamoDB table based on provided id and barcode
            var product = await _context.LoadAsync<Product>(id, barcode);
            // If the product is not found, return NotFound if true else return the product
            if (product == null) return NotFound();
            return Ok(product);
        }

        // HTTP GET method to retrieve ALL products
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Returns all products asynchronously from the DynamoDB table
            var products = await _context.ScanAsync<Product>(default).GetRemainingAsync();
            // Returns the retrieved products
            return Ok(products);
        }

        // HTTP POST method to create a new product
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(Product request)
        {
            // Loads a product asynchronously from the DynamoDB table based on provided id and barcode
            var product = await _context.LoadAsync<Product>(request.Id, request.Barcode);
            // Checking if product with same id already exists, returns BadRequest if true
            if (product != null) return BadRequest($"Product with Id {request.Id} and BarCode {request.Barcode} Already Exists");

            await _context.SaveAsync(request);
            // Returning the created product
            return Ok(request);
        }

        // HTTP DELETE method to delete a product by its id and barcode
        [HttpDelete("{id}/{barcode}")]
        public async Task<IActionResult> Delete(string id, string barcode)
        {
            // Loads a product asynchronously from the DynamoDB table based on provided id and barcode
            var product = await _context.LoadAsync<Product>(id, barcode);
            // Checking if the product is not found and returning NotFound if true
            if (product == null) return NotFound();
            // asynchronously deletes from the DynamoDB table
            await _context.DeleteAsync(product);
            // Returns NoContent to indicate successful deletion
            return NoContent();
        }

        // HTTP PUT method to update an existing product
        [HttpPut]
        public async Task<IActionResult> Update(Product request)
        {
            // Loads a product asynchronously from the DynamoDB table based on provided id and barcode
            var product = await _context.LoadAsync<Product>(request.Id, request.Barcode);
            // Checking if the product is not found and returning NotFound if true
            if (product == null) return NotFound();
            // Saving the updated product asynchronously to the DynamoDB table
            await _context.SaveAsync(request);
            // Returning the updated product
            return Ok(request);
        }
    }
}


