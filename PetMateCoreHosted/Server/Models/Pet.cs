using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Server.Models;
// connecting to our "pets" DynamoDBTable

[DynamoDBTable("pets")]

public class Pet
{
    [DynamoDBHashKey("id")]
    public string? Id { get; set; }

    [DynamoDBRangeKey("username")]
    public string? Username { get; set; }

    [DynamoDBProperty("name")]
    public string? Name { get; set; }

    [DynamoDBProperty("species")]
    public string? Species { get; set; }

    [DynamoDBProperty("description")]
    public string? Description { get; set; }

    [DynamoDBProperty("address")]
    public string? Address { get; set; }

    [DynamoDBProperty("photoUrl")]
    public string? PhotoUrl { get; set; }

    [DynamoDBProperty("startDate")]
    public string? StartDate { get; set; }

    [DynamoDBProperty("endDate")]
    public string? EndDate { get; set; }

    [DynamoDBProperty("price")]
    public string? Price { get; set; }

    [DynamoDBProperty("price_id")]
    public string? PriceId { get; set; }

    [DynamoDBProperty("status")]
    public string? Status { get; set; } //Created -> Payment -> Reserved -> Fulfilled

    [DynamoDBProperty("client")]
    public string? Client { get; set; }

    [DynamoDBProperty("comments")]
    public List<string>? Comments { get; set; }
}
