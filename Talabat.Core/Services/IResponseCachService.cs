using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponseCachService
    {
        Task CacheResponseAsync(string key, object response, TimeSpan timeToLife);
        Task<string> GetCacheResponseAsync(string cacheKey);

    }
}
