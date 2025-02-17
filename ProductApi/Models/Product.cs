﻿namespace ProductApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Inventory { get; set; } 
        public decimal Price { get; set; }
    }
}
