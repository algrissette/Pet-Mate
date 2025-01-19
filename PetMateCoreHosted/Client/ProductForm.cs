using System;
namespace PetMateCoreHosted.Client

// create a ProductForm class with same fields as our Product.cs model in Server folder 
{
    public class ProductForm
    {
        public string Id { get; set; } = "Id";
        public string Barcode { get; set; } = "Barcode";
        public string Name { get; set; } = "Name";
        public string Description { get; set; } = "Description";
        public decimal Price { get; set; }
    }
}


