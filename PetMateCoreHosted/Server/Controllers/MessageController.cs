using System;
using Amazon.DynamoDBv2.DataModel;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PetMateCoreHosted.Server.Models;

namespace PetMateCoreHosted.Server.Controllers
{
    // Define the api endpoint for controller
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        // Private field hold instance of the DynamoDB table
        private readonly IDynamoDBContext _context;

        // Constructor for MessageController, takes an IDynamoDBContext instance as a parameter
        // to be injected into the controller
        public MessageController(IDynamoDBContext context)
        {
            // Assigns the injected context to the private field
            _context = context;
        }

        [HttpGet("{fromuser}/{touser}")]
        public async Task<IActionResult> Get(string fromuser, string touser)
        {
            // Loading a Message asynchronously from the DynamoDB table based on provided id and barcode
            var message = await _context.LoadAsync<Message>(fromuser, touser);
            // If the Message is not found, return NotFound if true else return the Message
            if (message == null) return NotFound();
            return Ok(message);
        }

        [HttpGet("{fromuser}")]
        public async Task<IActionResult> Get(string fromuser)
        {
            // Loading a Message asynchronously from the DynamoDB table based on provided id and barcode
            var message = await _context.LoadAsync<Message>(fromuser);
            // If the Message is not found, return NotFound if true else return the Message
            if (message == null) return NotFound();
            return Ok(message);
        }

        // HTTP GET method to retrieve ALL Message
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Returns all Message asynchronously from the DynamoDB table
            var messages = await _context.ScanAsync<Message>(default).GetRemainingAsync();
            // Returns the retrieved Message
            return Ok(messages);
        }

        // HTTP POST method to create a new Message
        // Enabling CORS for this endpoint to allow Blazor front end to access endpoint
        [HttpPost]
        public async Task<IActionResult> Create(Message request)
        {
            // Loads a Message asynchronously from the DynamoDB table based on provided id and barcode
            var message = await _context.LoadAsync<Message>(request.FromUser, request.ToUser);
            await _context.SaveAsync(request);
            // Returning the created Message
            return Ok(request);
        }

    }
}

