using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Client
{
    public class MessageForm
    {
        public string? FromUser { get; set; }
        public string? Timestamp { get; set; }
        public string? ToUser { get; set; }
        public string? Content { get; set; }

    }

}

