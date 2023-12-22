using Sirena.Api.Domain;
using Sirena.Api.Domain.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Sirena.Api.Services
{
    public class CacheService: ICacheService
    {

        public int CacheSize {get;set;}
        public ConcurrentDictionary<string, Airport> Cache { get; set; }

        public CacheService(int cacheSize)
        {
            CacheSize = cacheSize;
            Cache = new ConcurrentDictionary<string, Airport>();
        }

        void Increment(string code)
        {
                Cache[code].Frequency += 1;
        }


        void Insert(Airport airport)
        {
            if (Cache.Count == CacheSize)
            {
              
                string lfuKey = FindLFU();
           
                Cache.TryRemove(lfuKey,out _);
            }

      
            Cache.TryAdd(airport.IataCode.Value, airport);
        
        }
        public Airport Get(string code)
        {
            return Cache.TryGetValue(code, out Airport airport) ? airport : null;
        }
        public void Add(Airport airport)
        {
            if (!Cache.ContainsKey(airport.IataCode.Value))
            {
                Insert(airport);
            }
  
        }

        public bool Contains(string code)
        {
            if (Cache.ContainsKey(code))
            {
                Increment(code);
                return true;
            }
            return false;
        }
        
        string FindLFU()
        {
            string lfuKey = string.Empty;
            int minFrequency = Int32.MaxValue;
            foreach (KeyValuePair<string, Airport> entry in Cache)
            {
                if (entry.Value.Frequency < minFrequency)
                {
                    minFrequency = entry.Value.Frequency;
                    lfuKey = entry.Key;
                }
            }
            return lfuKey;
        }
    }
}

