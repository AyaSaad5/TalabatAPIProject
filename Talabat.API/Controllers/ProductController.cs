using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.API.DTOs;
using Talabat.API.Helpers;
using Talabat.Core.Enitites;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;

namespace Talabat.API.Controllers
{

    public class ProductController : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> productsRepo,
                                 IGenericRepository<ProductBrand> brandsRepo,
                                 IGenericRepository<ProductType> typesRepo,IMapper mapper)
        {
            _productsRepo = productsRepo;
            _brandsRepo = brandsRepo;
            _typesRepo = typesRepo;
            _mapper = mapper;
        }

      
        //[CachedAttribute(600)]

        [HttpGet] // Get : api/Product
        public async Task<ActionResult<Pagination<ProductToreturnDTO>>> GetProducts([FromQuery] ProductSpecParams productParams)
         {
            var spec = new ProductWithBrand_Type(productParams);
            var products = await _productsRepo.GetAllWithSpecAsync(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToreturnDTO>>(products);

            var countSpec = new ProductWithFiltersForCountSpec(productParams);
            var count = await _productsRepo.GetCountAsync(countSpec);

            return Ok(new Pagination<ProductToreturnDTO>(productParams.PageIndex, productParams.PageSize,count,Data));
         }
        [CachedAttribute(600)]

        [HttpGet("{id}")] //Get : api/Product/id
        public async Task <ActionResult<ProductToreturnDTO>> GetProductbyId(int id)
        {
            var product = await _productsRepo.GetByIdAsync(id) ;
            if(product == null)
                return NotFound();
           return Ok(_mapper.Map<Product,ProductToreturnDTO>(product));
        }

        [CachedAttribute(600)]

        [HttpGet("brands")] // Get : api/product/brands
        public async Task <ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandsRepo.GetAllAsync() ;
            return Ok(brands);
        }

        [CachedAttribute(600)]

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _typesRepo.GetAllAsync();
            return Ok(types);
        }
    }
}
