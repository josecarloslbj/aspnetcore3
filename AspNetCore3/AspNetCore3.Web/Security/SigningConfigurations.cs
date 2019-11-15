using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore3.Web.Security
{
    public class SigningConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }

        public string Seconds { get; set; }
        public string JWT_Secret { get; set; }

        public string Client_URL { get; set; }

        //public SigningConfigurations(string chave)
        //{


        //    //Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
        //    //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave)),
        //    //    SecurityAlgorithms.HmacSha256Signature);



        //    Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));

        //    SigningCredentials = new SigningCredentials(
        //        Key, SecurityAlgorithms.RsaSha256Signature);
        //}
    }
}
