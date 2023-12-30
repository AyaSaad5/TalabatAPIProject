using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Enitites;
using Talabat.Core.Enitites.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class TalabatDbContextSeed
    {
        public static async Task Seedasync(TalabatDbContext context, ILoggerFactory loggerfactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var brand in brands)
                    {
                        context.Set<ProductBrand>().Add(brand);
                    }
            
                }
                if (!context.ProductTypes.Any())
                {
                    var typeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    foreach (var type in types)
                    {
                        context.Set<ProductType>().Add(type);
                    }
               
                }
                if (!context.Products.Any())
                {
                    var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    foreach (var product in products)
                    {
                        context.Set<Product>().Add(product);
                    }
                }

                if (!context.DeliveryMethods.Any())
                {
                    var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                    var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        context.Set<DeliveryMethod>().Add(deliveryMethod);
                    }

                }


                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<TalabatDbContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
