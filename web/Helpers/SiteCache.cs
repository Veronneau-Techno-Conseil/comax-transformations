using CommunAxiom.Transformations.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CommunAxiom.Transformations.AppModel.Business;

namespace web.Helpers
{
    public class SiteCache
    {
        IServiceProvider _provider;
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        
        public SiteCache(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public IEnumerable<ModuleType> GetModuleTypes()
        {
            return _cache.GetOrCreate<IEnumerable<ModuleType>>("ModuleTypes", entry =>
            {
                using var scope = this._provider.CreateScope();
                var moduleBusiness = scope.ServiceProvider.GetService<IModuleBusiness>();
                var res = moduleBusiness.GetModuleTypes();
                var types = res.ToListAsync().GetAwaiter().GetResult();
                return types;
            });
        }
    }
}
