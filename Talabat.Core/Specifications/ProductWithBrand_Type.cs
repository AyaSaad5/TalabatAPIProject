using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrand_Type : BaseSpecification<Product>
    {
        public ProductWithBrand_Type(ProductSpecParams productParams) 
            : base(P =>(string.IsNullOrEmpty(productParams.Search) || P.Name.ToLower().Contains(productParams.Search)) &&
                   (!productParams.BrandId.HasValue || P.ProductBrandId == productParams.BrandId.Value) &&
                   (!productParams.TypeId.HasValue || P.ProductTypeId == productParams.TypeId.Value))
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            Applypagination(productParams .PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
        }
        public ProductWithBrand_Type(int id) : base(P => P.Id == id) 
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
