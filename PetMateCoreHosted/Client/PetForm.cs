using System;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;

namespace PetMateCoreHosted.Client
{
    public class PetForm
    {
        public string Id { get; set; } = "Id";
        public string Username { get; set; } = "Username";
        public string Name { get; set; } = "Name";
        public string Address { get; set; } = "Address";
        public string Description { get; set; } = "Description";
        public string Species { get; set; } = "Species";
        public IFormFile? PhotoUrl { get; set; }
        public string PhotoUrlString { get; set; } = "Default";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string Price { get; set; } = "Price";
        public string PriceId { get; set; } = "";
        public string Status { get; set; } = "Created";  //Created -> Payment -> Reserved -> Fulfilled
        public string Client { get; set; } = "";
    }
}



