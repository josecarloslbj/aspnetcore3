using AspNetCore3.Web.Model;
using AspNetCore3.Web.Repository;
using AspNetCore3.Web.Security;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore3.Web.Controllers
{
    [Route("api/[controller]")]
    [Route("cadastrar")]
    public class LoginController : Controller
    {
        private readonly SigningConfigurations _appSettings;
        private readonly IConfiguration _configuration;


        public LoginController(IOptions<SigningConfigurations> appSettings,
            IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]       
        public object Post(
            [FromBody]User usuario,
            [FromServices]UsersDAO usersDAO,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {

            var jobDelayed = BackgroundJob.Schedule(() => teste(), TimeSpan.FromSeconds(30));

            bool credenciaisValidas = false;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.UserID))
            {
                var usuarioBase = usersDAO.Find(usuario.UserID);
                credenciaisValidas = (usuarioBase != null &&
                    usuario.UserID == usuarioBase.UserID &&
                    usuario.AccessKey == usuarioBase.AccessKey);
            }

            if (credenciaisValidas)
            {
                //SigningConfigurations token  = new SigningConfigurations();
                //token.JWT_Secret = _configuration["TokenConfigurations:JWT_Secret"].ToString();
                //token.Audience = _configuration["TokenConfigurations:Audience"].ToString();
                //token.Issuer = _configuration["TokenConfigurations:Issuer"].ToString();
                //token.Seconds = _configuration["TokenConfigurations:Seconds"].ToString();
                //token.Client_URL = _configuration["TokenConfigurations:Client_URL"].ToString();


                //return Jose.JWT.Encode(token, Encoding.UTF8.GetBytes("abcdefghijklmnopqrs"), Jose.JwsAlgorithm.HS256);


                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.UserID, "Login"),
                    new[] {
                        new Claim("NroAleatorio", Guid.NewGuid().ToString("N")),
                        new Claim("IdUsuario", usuario.UserID)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);


                var key = Encoding.UTF8.GetBytes(_configuration["TokenConfigurations:JWT_Secret"].ToString());


                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("cadastrar")]
        public void cadastrar([FromBody]User usuario)
        {



        }


        public void teste()
        {

        }
    }

}
