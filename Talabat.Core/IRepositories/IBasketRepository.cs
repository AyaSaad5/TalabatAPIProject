﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;

namespace Talabat.Core.IRepositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdatebasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasket(string basketId);
    }
}
