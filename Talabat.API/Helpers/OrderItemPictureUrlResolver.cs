using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks.Sources;
using Talabat.API.DTOs;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.API.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem,OrderItemDTO,string>
    {
        public IConfiguration Configration { get; }
        public OrderItemPictureUrlResolver(IConfiguration configration)
        {
            Configration = configration;
        }

   

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Product.PictureUrl))
                return $"{Configration["BaseApiUrl"]}{source.Product.PictureUrl}";
            return null;
        }
    }
}
