using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core.IRepositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;
namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IResponseCachService,ResponsecachService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddAutoMapper(typeof(MappingProfiles));
           
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                                          .SelectMany(M => M.Value.Errors)
                                                          .Select(E => E.ErrorMessage)
                                                          .ToArray();
                    var validationErrorResponse = new ApivalidationErrorsResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            return services;
        }
    }
}
