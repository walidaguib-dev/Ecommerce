using System.Text;
using Ecommerce.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;


namespace Ecommerce.Repositories
{
    public class CacheRepo : ICache
    {


        private readonly IDistributedCache cacheService;

        public CacheRepo(IDistributedCache _cache)
        {
            cacheService = _cache;
        }

        public async Task<T?> GetFromCacheAsync<T>(string key)
        {
            var cachedData = await cacheService.GetAsync(key);
            if (cachedData.IsNullOrEmpty()) return default;
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cachedData), new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });


        }

        public Task RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveCaching(string key)
        {
            await cacheService.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T values)
        {
            var serializedData = JsonConvert.SerializeObject(values, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            try
            {

                var data = Encoding.UTF8.GetBytes(serializedData);
                await cacheService.SetAsync(key, data, new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30)
                });
            }
            catch (Exception ex)
            {
                // Handle Redis operations errors appropriately
                Console.WriteLine($"Error setting cache: {ex.Message}");
            }
        }





    }
}
