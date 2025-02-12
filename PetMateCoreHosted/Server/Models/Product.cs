﻿using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Server.Models;

// connecting to our "products" DynamoDBTable 
[DynamoDBTable("products")]
public class Product
{
    [DynamoDBHashKey("id")]
    public string? Id { get; set; }
    [DynamoDBRangeKey("barcode")]
    public string? Barcode { get; set; }
    [DynamoDBProperty("name")]
    public string? Name { get; set; }
    [DynamoDBProperty("description")]
    public string? Description { get; set; }
    [DynamoDBProperty("price")]
    public decimal Price { get; set; }
}