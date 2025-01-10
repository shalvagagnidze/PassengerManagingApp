using Microsoft.Extensions.Caching.Memory;
using Service.Interfaces;
using Service.Models.WebScrappingModels;
using System;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CachedTimeTableService : ITimeTableService
    {
        private readonly TimeTableService _timeTableService;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "TimeTable_";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5); // Cache for 5 minutes

        public CachedTimeTableService(HttpClient httpClient, IMemoryCache cache)
        {
            _timeTableService = new TimeTableService(httpClient);
            _cache = cache;
        }

        public async Task<TimeTableResponse> GetTimeTableAsync(string city)
        {
            var cacheKey = $"{CacheKey}{city}";

            if (!_cache.TryGetValue(cacheKey, out TimeTableResponse cachedResponse))
            {
                cachedResponse = await _timeTableService.GetTimeTableAsync(city);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(GetNextCacheExpiration())
                    .SetSlidingExpiration(CacheDuration);

                _cache.Set(cacheKey, cachedResponse, cacheEntryOptions);
            }

            return cachedResponse;
        }

        private DateTime GetNextCacheExpiration()
        {
            var now = DateTime.Now;
            var sixThirtyToday = DateTime.Today.AddHours(6).AddMinutes(31);

            // If it's after 6:31 AM, expire cache at 6:31 AM tomorrow
            if (now > sixThirtyToday)
            {
                return sixThirtyToday.AddDays(1);
            }

            // If it's before 6:31 AM, expire cache at 6:31 AM today
            return sixThirtyToday;
        }
    }
}