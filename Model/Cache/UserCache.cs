using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;
using MemoryCache = System.Runtime.Caching.MemoryCache;

namespace Es.Udc.DotNet.PracticaMaD.Model.Cache
{
    public static class UserCache
    {
        private static MemoryCache cache;
        private static List<string> keys = new List<string>();
        private static readonly int MAX_SIZE = 5;
        static UserCache()
        {
            cache = new MemoryCache("UserCache");
        }

        public static void Add(string cacheKey, object value)
        {
            cache.Add(cacheKey, value, new CacheItemPolicy());
            keys.Add(cacheKey);
            if (keys.Count > MAX_SIZE)
            {
                var key = keys[0];
                cache.Remove(key);
                keys.Remove(key);
            }
        }

        public static bool Exists(string cacheKey)
        {
            return cache.Contains(cacheKey);
        }

        public static E Get<E>(string cacheKey)
        {
            return (E)cache.Get(cacheKey);
        }

        public static void Remove(string cacheKey)
        {
            cache.Remove(cacheKey);
            keys.Remove(cacheKey);
        }
        public static void Dispose()
        {
            cache.Dispose();
            keys.Clear();
        }

    }
}
