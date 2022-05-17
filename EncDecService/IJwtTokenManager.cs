using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncDecService
{
    public interface IJwtTokenManager
    {
        string Authenticate(string username, string password);
    }
}
