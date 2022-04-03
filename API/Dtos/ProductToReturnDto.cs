using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } =default!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = default!;
        public string ProductType { get; set; } =default!;
        public string ProductBrand { get; set; } = default!;
    }
}