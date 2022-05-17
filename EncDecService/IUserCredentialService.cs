using EncDecService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncDecService
{
    public interface IUserCredentialService
    {
        List<UserCredential> GetUserCredential();
    }
}
