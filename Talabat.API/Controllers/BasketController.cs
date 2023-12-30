using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.API.DTOs;
using Talabat.Core.Enitites;
using Talabat.Core.IRepositories;

namespace Talabat.API.Controllers
{
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet("{id}")] //Get : /api/basket/1
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost] //Post : /api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDTO basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTO,CustomerBasket>(basket);
           var createdOrupdated = await _basketRepo.UpdatebasketAsync(mappedBasket);
            return Ok(createdOrupdated);
        }

        [HttpDelete] // Delete : /api/basket
        public async Task DeleteBasket(string id)
        {
            await _basketRepo.DeleteBasket(id);
        }
    }
}
