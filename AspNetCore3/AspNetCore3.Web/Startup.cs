using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore3.Web.Repository;
using AspNetCore3.Web.Security;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore3.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var chave = (Configuration["TokenConfigurations:JWT_Secret"].ToString());




            //Setup data migrations 
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    //.AddSQLite()
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString("Data Source=localhost;Initial Catalog=ExemploJWT;Integrated Security=True")
                    // Define the assemblies containing the migrations
                    .ScanIn(typeof(AspNetCore3.DatabaseMigration.MigrationsAssembly).Assembly).For.Migrations()
                   
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());



            /*Quanto a UsersDAO, o método AddTransient determina que referências desta classe sejam geradas toda vez que uma dependência for encontrada;*/
            services.AddTransient<UsersDAO>();

            /*Instâncias dos tipos SigningConfigurations e TokenConfigurations serão configuradas via método AddSingleton, de forma que uma única referência das mesmas seja empregada durante todo o tempo em que a aplicação permanecer em execução.*/
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            //Inject AppSettings
            services.Configure<SigningConfigurations>(Configuration.GetSection("TokenConfigurations"));

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);


            services.AddSingleton(tokenConfigurations);


            var key = Encoding.UTF8.GetBytes(Configuration["TokenConfigurations:JWT_Secret"].ToString());


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(key); // signingConfigurations.Key;
                // paramsValidation.ValidAudience = tokenConfigurations.Audience;
                // paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                paramsValidation.ValidateIssuer = false;
                paramsValidation.ValidateAudience = false;


                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddMvc();

            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}          

            //app.UseMvc();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
              builder.WithOrigins(Configuration["TokenConfigurations:Client_URL"].ToString())
              .AllowAnyHeader()
              .AllowAnyMethod()
              );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Run database migrations 
            migrationRunner.MigrateUp();

        }
    }
}
