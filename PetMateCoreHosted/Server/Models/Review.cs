using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Server.Models;

// connecting to our "products" DynamoDBTable 
[DynamoDBTable("reviews")]
public class Review
{
    [DynamoDBHashKey("username")]
    public string? Username { get; set; }

    [DynamoDBProperty("temperament")]
    public double? Temperament { get; set; }

    [DynamoDBProperty("attention")]
    public double? Attention { get; set; }

    [DynamoDBProperty("activity")]
    public double? Activity { get; set; }

    [DynamoDBProperty("enjoyment")]
    public double? Enjoyment { get; set; }

    [DynamoDBProperty("count")]
    public double? Count { get; set; }

    [DynamoDBProperty("comment")]
    public string? Comment { get; set; }
}
