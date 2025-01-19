using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Server.Models;
// connecting to our "pets" DynamoDBTable

[DynamoDBTable("messages")]
public class Message
{
    [DynamoDBHashKey("fromuser")]
    public string? FromUser { get; set; }
    [DynamoDBRangeKey("timestamp")]
    public string? Timestamp { get; set; }
    [DynamoDBProperty("touser")]
    public string? ToUser { get; set; }
    [DynamoDBProperty("content")]
    public string? Content { get; set; }

}

