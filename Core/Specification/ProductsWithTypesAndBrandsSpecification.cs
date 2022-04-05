using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productPramas)
            : base(x =>
                (string.IsNullOrEmpty(productPramas.Search) || 
                x.Name.ToLower().Contains(productPramas.Search)) &&
                (!productPramas.brandId.HasValue || x.ProductBrandId == productPramas.brandId) &&  
                (!productPramas.typeId.HasValue || x.ProductTypeId == productPramas.typeId)   
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productPramas.PageSize * (productPramas.PageIndex - 1),
            productPramas.PageSize);

            if(!string.IsNullOrEmpty(productPramas.Sort))
            {
                switch(productPramas.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}