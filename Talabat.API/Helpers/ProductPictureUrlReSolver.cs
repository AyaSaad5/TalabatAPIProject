using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.API.DTOs;
using Talabat.Core.Enitites;

namespace Talabat.API.Helpers
{
    public class ProductPictureUrlReSolver : IValueResolver<Product, ProductToreturnDTO, string>
    {
        public IConfiguration Configuration { get;}

        public ProductPictureUrlReSolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Resolve(Product source, ProductToreturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["BaseApiUrl"]}{source.PictureUrl}";
            return null;
        }
    }
}
