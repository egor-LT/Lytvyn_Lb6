using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public class FunctionCache<TKey, TResult>
    {
        private readonly Dictionary<TKey, Tuple<TResult, DateTime>> cache = new Dictionary<TKey, Tuple<TResult, DateTime>>();
        private readonly TimeSpan cacheDuration;

        public FunctionCache(TimeSpan cacheDuration)
        {
            this.cacheDuration = cacheDuration;
        }

        public TResult GetOrCreate(TKey key, Func<TKey, TResult> function)
        {
            if (cache.TryGetValue(key, out var cachedItem))
            {
                if ((DateTime.Now - cachedItem.Item2) < cacheDuration)
                {
                    return cachedItem.Item1;
                }
                else
                {
                    cache.Remove(key);
                }
            }

            TResult result = function(key);
            cache[key] = Tuple.Create(result, DateTime.Now);
            return result;
        }

        public void ClearCache()
        {
            cache.Clear();
        }
    }
}