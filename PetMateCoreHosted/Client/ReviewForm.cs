using System;
using Amazon.DynamoDBv2.DataModel;

namespace PetMateCoreHosted.Client
{
    public class ReviewForm
    {
        public double Username { get; set; }
        public double Temperament { get; set; } 
        public double Attention { get; set; } 
        public double Activity { get; set; } 
        public double Enjoyment { get; set; }
        public double Count { get; set; }
    }
}

