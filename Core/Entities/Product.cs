namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } =default!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = default!;
        public ProductType ProductType { get; set; } =default!;
        public int ProductTypeId { get; set; }
        public ProductBrand ProductBrand { get; set; } = default!;
        public int ProductBrandId { get; set; }
        
    }
}