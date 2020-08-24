using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisCacheSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("Set")]
        public async Task<JsonResult> SetCache(string key, string value, [FromServices] IDistributedCache cache)
        {
            await cache.SetStringAsync(key, value);
            return new JsonResult(new { key = key, value = value });
        }

        [HttpGet("Get")]
        public async Task<JsonResult> GetCache(string key, [FromServices] IDistributedCache cache)
        {
            string value = await cache.GetStringAsync(key);
            return new JsonResult(new { key = key, value = value });
        }

        [HttpGet("SetWithExpiration")]
        public async Task<JsonResult> SetCacheWithExpiration(string key, string value, int slidingExpirationInSecond, [FromServices] IDistributedCache cache)
        {
            var options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationInSecond));
            await cache.SetStringAsync(key, value, options);
            return new JsonResult(new { key = key, value = value, SlidingExpirationInSecond = slidingExpirationInSecond });
        }

        [HttpGet("GetWithRefresh")]
        public async Task<JsonResult> GetCacheWithRefresh(string key, [FromServices] IDistributedCache cache)
        {
            await cache.RefreshAsync(key);
            string value = await cache.GetStringAsync(key);

            return new JsonResult(new { key = key, value = value });
        }

        [HttpGet("Remove")]
        public async Task<JsonResult> RemoveCache(string key, [FromServices] IDistributedCache cache)
        {
            string value = await cache.GetStringAsync(key);
            await cache.RemoveAsync(key);

            return new JsonResult(new { key = key, value = value });
        }
    }
}
