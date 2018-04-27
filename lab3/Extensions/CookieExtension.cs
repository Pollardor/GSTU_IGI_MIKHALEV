using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Extensions
{
    public static class CookieExtension
    {
        public static void Set<T>(this IResponseCookies cookies, string key, T value)
        {
            cookies.Append(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this IRequestCookieCollection cookies, string key)
        {
            var value = cookies[key];
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
