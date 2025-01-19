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
    public class ReviewController : ControllerBase
    {
        // Private field hold instance of the DynamoDB table
        private readonly IDynamoDBContext _context;

        // Constructor for ProductsController, takes an IDynamoDBContext instance as a parameter
        // to be injected into the controller
        public ReviewController(IDynamoDBContext context)
        {
            // Assigns the injected context to the private field
            _context = context;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            // Loading a product asynchronously from the DynamoDB table based on provided id and barcode
            var review = await _context.LoadAsync<Review>(username);
            // If the product is not found, return NotFound if true else return the product
            if (review == null) return NotFound();
            return Ok(review);
        }

        // HTTP GET method to retrieve ALL products
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Returns all products asynchronously from the DynamoDB table
            var reviews = await _context.ScanAsync<Review>(default).GetRemainingAsync();
            // Returns the retrieved products
            return Ok(reviews);
        }

        // HTTP POST method to create a new product
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(Review request)
        {
            try
            {
                // Check if a review already exists for the given username
                var existingReview = await _context.LoadAsync<Review>(request.Username);

                if (existingReview != null)
                {
                    // If a review already exists, update it with the new data
                    existingReview.Temperament = request.Temperament;
                    existingReview.Attention = request.Attention;
                    existingReview.Activity = request.Activity;
                    existingReview.Enjoyment = request.Enjoyment;
                    existingReview.Comment = request.Comment;
                    existingReview.Count++; // Increment the count

                    // Save the updated review to the database
                    await _context.SaveAsync(existingReview);

                    // Return the updated review
                    return Ok(existingReview);
                }
                else
                {
                    // If no review exists for the given username, create a new one
                    await _context.SaveAsync(request);

                    // Return the newly created review
                    return Ok(request);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, $"An error occurred while creating the review: {ex.Message}");
            }
        }




    }
}



