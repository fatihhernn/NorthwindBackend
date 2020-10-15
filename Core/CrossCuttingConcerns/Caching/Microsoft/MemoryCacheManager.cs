using Core.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using System.Linq;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //ilk önce servis koleksiyonuna ekle bunu;

        private IMemoryCache _cache;
        public MemoryCacheManager()
        {
            //servis altyapımıza erişelim
            _cache=ServiceTool.ServiceProvider.GetService<IMemoryCache>();//bana 1 tane IMemoryCache türünden nesne ver
        }

        public void Add(string key, object data, int duration)
        {
            _cache.Set(key, data, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _cache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _cache.TryGetValue(key, out _);//bu değer döndürmeyi bekliyor ama bizim için önemli değil, var mı yok mu diye kontrol etsek yeter.
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)//.net core , memory cache listesini vermiyor
        {

            //.net core bize cache listesini vermiyor. stackover flow da aşağıdaki kodlar bulunarak bunlar listelenip silindi...

            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_cache) as dynamic;


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {

                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);


                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _cache.Remove(key);
            }
        }

        
    }
}
