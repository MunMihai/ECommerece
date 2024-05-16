using Ecommerce.Core.Enums;

namespace Ecommerce.Core.Entities
{
    public class Product
    {
        public Product()
        {
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int Inventory { get; set; }
        public UnitType UnitType { get; set; }
        public string ImageUrl { get; set; }
    }
}