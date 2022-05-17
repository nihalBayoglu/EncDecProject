using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EncDecService
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly IConfiguration _configuration;
        IKeyPassInMemoryCache _keyPassInMemoryCache;
        public JwtTokenManager(IConfiguration configuration,IKeyPassInMemoryCache keyPassInMemoryCache)
        {
            _configuration = configuration;
            _keyPassInMemoryCache = keyPassInMemoryCache;
        }
        //public string Authenticate(string username, string password)
        //{
        //    if (!Data.Users.Any(x => x.Key.Equals(username) && x.Value.Equals(password)))
        //        return null;

        //    var key = _configuration.GetValue<string>("JwtConfig:Key");
        //    var keyBytes = Encoding.ASCII.GetBytes(key);

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim (ClaimTypes.NameIdentifier,username)
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(30), //30sn sonra token geçersiz
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)  //token'ı encrypt etmek için token handler kullanır

        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);  //Token'dan string' serilize eder.
        //}

        public string Authenticate(string username, string password)
        {
            if(!_keyPassInMemoryCache.GetUserCredentials().Any(x => x.UserName.Equals(username) && x.Password.Equals(password)))
                return null;

            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim (ClaimTypes.NameIdentifier,username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30), //30dk sonra token geçersiz
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)  //token'ı encrypt etmek için token handler kullanır

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);  //Token'dan string' serilize eder.
        }
    }
}
