using IGILab1Norm;
using lab2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Middleware
{
    public class CacheMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private string _cacheKey;

        public CacheMiddleware(RequestDelegate next, IMemoryCache memoryCache, string cacheKey)
        {
            _next = next;
            _memoryCache = memoryCache;
            _cacheKey = cacheKey;
        }

        public Task Invoke(HttpContext httpContext, RentContext _db)
        {
            CacheViewModel indexViewModel = new CacheViewModel();
            if (!_memoryCache.TryGetValue(_cacheKey, out indexViewModel))
            {
                var clients = _db.Clients
                    .OrderBy(t => t.Id)
                    .Take(10)
                    .ToList();
                var cars = _db.Cars
                    .OrderBy(t => t.CarID)
                    .Take(10)
                    .ToList();
                var rents = _db.Rents
                    .OrderBy(t => t.RentID)
                    .Include(t => t.Client)
                    .Include(t => t.Car)
                    .Take(10)
                    .ToList();

                indexViewModel = new CacheViewModel { Clients = clients, Cars = cars, Rents = rents };
                _memoryCache.Set(_cacheKey, indexViewModel,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(2 * 10 + 240)));
            }

            return _next(httpContext);
        }
    }


    public static class CacheMiddlewareExtensions
    {
        public static IApplicationBuilder UseOperatinCache(this IApplicationBuilder builder, string cacheKey)
        {
            return builder.UseMiddleware<CacheMiddleware>(cacheKey);
        }
    }
}
