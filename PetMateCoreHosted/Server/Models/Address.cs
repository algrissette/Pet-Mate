using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Server.Models;

[DynamoDBTable("addresses")]
public class Address
{
    [DynamoDBHashKey("username")]
    public string? Username { get; set; }
    [DynamoDBProperty("address")]
    public string? UserAddress { get; set; }
}
