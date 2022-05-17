using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptUncryptService
{
    public interface ICheckToken
    {
        bool ValidateToken(string authToken);
    }
}
