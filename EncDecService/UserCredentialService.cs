using EncDecService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncDecService
{
    public class UserCredentialService : IUserCredentialService
    {
        public List<UserCredential> GetUserCredential()
        {
            var userCredentials = new List<UserCredential>{
                new UserCredential
                {
                    UserName = "userName2",
                    Password = "pass2"
                },
                new UserCredential
                {
                    UserName = "userName3",
                    Password = "pass3"
                },
            };
            return userCredentials;
        }
    }
}
