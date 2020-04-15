using AspNetCore3.Domain.Contracts;
using AspNetCore3.Domain.Entities;
using AspNetCore3.Web.Model;
using AspNetCore3.Web.Repository;
using AspNetCore3.Web.Security;
using Hangfire;
using Jose;
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
        private readonly IUsuarioRepository _usuarioRepository;


        public LoginController(IOptions<SigningConfigurations> appSettings,
            IConfiguration configuration,
            IUsuarioRepository usuarioRepository)
        {
            _appSettings = appSettings.Value;
            _configuration = configuration;
            _usuarioRepository = usuarioRepository;
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
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Nome))
            {
                var usuarioBase = usersDAO.Find(usuario.Nome);
                credenciaisValidas = (usuarioBase != null &&
                    usuario.Nome == usuarioBase.Nome &&
                    usuario.Senha == usuarioBase.Senha);
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
                    new GenericIdentity(usuario.Login, "Login"),
                    new[] {
                        new Claim("NroAleatorio", Guid.NewGuid().ToString("N")),
                        new Claim("IdUsuario", usuario.Login)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);


                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });


                //var key = Encoding.UTF8.GetBytes(_configuration["TokenConfigurations:JWT_Secret"].ToString());


                //var handler = new JwtSecurityTokenHandler();
                //var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                //{
                //    Issuer = tokenConfigurations.Issuer,
                //    Audience = tokenConfigurations.Audience,
                //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                //    Subject = identity,
                //    NotBefore = dataCriacao,
                //    Expires = dataExpiracao
                //});
                //var token = handler.WriteToken(securityToken);

                //return new
                //{
                //    authenticated = true,
                //    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                //    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                //    accessToken = token,
                //    message = "OK"
                //};
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


        /// <summary>
        /// http://localhost:5002/cadastrar/cadastrar
        /// SSL hhtps://localhsot:porta /cadastrar/cadastrar
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("cadastrar")]
        public object cadastrar([FromBody]Usuario usuario)
        {

            //_usuarioRepository.CriarUsuario(usuario);
            var tokenString = GerarTokenJWT();
            return Ok(new { token = tokenString });

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("teste")]
        public object teste()
        {
            return Ok();
        }


        private string GerarTokenJWT()
        {
            var secretKey = Encoding.UTF8.GetBytes(_configuration["TokenConfigurations:JWT_Secret"].ToString());


            var issuer = _configuration["TokenConfigurations:Issuer"];
            var audience = _configuration["TokenConfigurations:Audience"];

            var payload = new Dictionary<string, object>()
        {
            { "env", "Development" },
            { "id", "1342" },
            { "sub", "corp\\lee.western" },  /* netwotk user */
			{ "lab", "36@PR" },
            { "iss", issuer},
            { "aud", audience },
            { "nbf",  DateTime.Now.AddMinutes(30) }, /* please see the fiddle to see how to generate these value - https://dotnetfiddle.net/z4jTFn*/
			{ "exp",  DateTime.Now.AddMinutes(30) } /*same as above*/
		};

            //Add Claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, "data"),
                new Claim(JwtRegisteredClaimNames.Sub, "data"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };


            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(secretKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // var token = new JwtSecurityToken(expires: expiry, signingCredentials: credentials);
            var token = new JwtSecurityToken(
               issuer,
           audience,
           claims,
           expires: DateTime.Now.AddMinutes(30),
           signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;




            //AuthorizationToken tt = new AuthorizationToken(issuer, audience, "DesenvolvedorNinja", null);

            //string token = Jose.JWT.Encode(tt, Encoding.UTF8.GetBytes(_configuration["TokenConfigurations:JWT_Secret"].ToString()), Jose.JwsAlgorithm.HS256);
            //return token;



            //string token = Jose.JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
            //return token;
        }




        [AllowAnonymous]
        [HttpPost]
        [Route("decode")]
        public object decode(string token)
        {
            var secretKey = Encoding.UTF8.GetBytes(_configuration["TokenConfigurations:JWT_Secret"].ToString());
            string json = Jose.JWT.Decode(token, secretKey);

            return json;
        }

    }

}
