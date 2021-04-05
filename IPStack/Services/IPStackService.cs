using IPStackDLL.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPStackDLL.Services
{
    public class IPStackService
    {
        private readonly IMemoryCache _cache;
        private IPDetailsDataProvider _dataProvider;

        public IPStackService(IMemoryCache memoryCache, IPDetailsDataProvider dataProvider)
        {

            _cache = memoryCache;
            _dataProvider = dataProvider;
        }
        private IEnumerable<IEnumerable<IPDetails>> IntoBatches<IPDetails>(IEnumerable<IPDetails> list, int size)
        {
            var rest = list;
            while (rest.Any())
            {
                yield return rest.Take(size);
                rest = rest.Skip(size);
            }
        }

        public Task PostData(List<IPDetails> values, Guid guid)
        {
            return Task.Run(() => {
                var batches = IntoBatches(values, 10);
                foreach (IEnumerable<IPDetails> batch in batches)
                {
                    for (int i = 0; i < values.Count; i++)
                    {
                        values[i].ipGuid = guid;
                        _dataProvider.SaveIPDetails(values[i]);
                        _cache.Set(values[i].ip, values[i], TimeSpan.FromSeconds(60));

                    }

                }

            });

        }

    }
}