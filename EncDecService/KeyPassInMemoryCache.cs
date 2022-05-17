using EncDecService.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncDecService
{
    public class KeyPassInMemoryCache : IKeyPassInMemoryCache
    {
        private IMemoryCache _memoryCache;
        private readonly IUserCredentialService _userCredentialService;

        public KeyPassInMemoryCache(IMemoryCache memoryCache, IUserCredentialService userCredentialService)
        {
            _memoryCache = memoryCache;
            _userCredentialService = userCredentialService;
        }

        public List<UserCredential> GetUserCredentials()
        {
            var cacheKey = "crypto";

            if (_memoryCache.TryGetValue(cacheKey, out List<UserCredential> userCredentialList))
                return userCredentialList;
            
            else
            {
                var userCredentials = _userCredentialService.GetUserCredential();
                
                _memoryCache.Set(cacheKey, userCredentials, TimeSpan.FromMinutes(1));

                return userCredentials;
            }
        }
    }
}
