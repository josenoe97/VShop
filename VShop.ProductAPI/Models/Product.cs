﻿namespace VShop.ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public string? ImagemURL { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
