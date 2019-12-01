using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore3.Domain.Contracts;
using AspNetCore3.Repository;
using AspNetCore3.Web.Repository;
using AspNetCore3.Web.Security;
using FluentMigrator.Runner;
using Hangfire;
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

            //Dapper Map
            RegisterMappings.Register();
           
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var chave = (Configuration["TokenConfigurations:JWT_Secret"].ToString());
            var conexao = (Configuration["ConnectionStrings:ExemploJWT"].ToString());


            services.AddSingleton(Configuration);
            //Dapper Map
            services.AddSingleton(Contexto.conexao= conexao );          
            services.AddSingleton(new RegisterMappings());

            //services.AddSingleton<Domain.Contracts.IRepositoryBase, AspNetCore3.Repository.RepositoryBase>();

            var sqlData = "Data Source=JOSE-RYZEN;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //Setup data migrations 
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    //.AddSQLite()
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(conexao)
                    // Define the assemblies containing the migrations
                    .ScanIn(typeof(AspNetCore3.DatabaseMigration.MigrationsAssembly).Assembly).For.Migrations()
                   
                ).AddLogging(lb => lb
                    .AddFluentMigratorConsole());



            /*Quanto a UsersDAO, o m�todo AddTransient determina que refer�ncias desta classe sejam geradas toda vez que uma depend�ncia for encontrada;*/
            services.AddTransient<UsersDAO>();

            /*Inst�ncias dos tipos SigningConfigurations e TokenConfigurations ser�o configuradas via m�todo AddSingleton, de forma que uma �nica refer�ncia das mesmas seja empregada durante todo o tempo em que a aplica��o permanecer em execu��o.*/
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            //Inject AppSettings
            services.Configure<SigningConfigurations>(Configuration.GetSection("TokenConfigurations"));



            /*
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

                // Verifica se um token recebido ainda � v�lido
                paramsValidation.ValidateLifetime = true;

                // Tempo de toler�ncia para a expira��o de um token (utilizado
                // caso haja problemas de sincronismo de hor�rio entre diferentes
                // computadores envolvidos no processo de comunica��o)
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


            */

            //JOSE JWT
            HabilidarEsquemaAutenticacao(services);



            services.AddHangfire(x => x.UseSqlServerStorage(conexao));


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

            //JOSE JWT
            EsquemaDeAutenticacao(app);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Run database migrations 
            migrationRunner.MigrateUp();


           // loggerFactory.AddConsole(Configuration.GetSection("Logging"));
           // loggerFactory.AddDebug();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }


        //Habilitando o esquema da autentica��o JWT no Startup
        //http://www.macoratti.net/19/04/aspncore_jwt1.htm
        public void HabilidarEsquemaAutenticacao(IServiceCollection services)
        {
            var key = Encoding.UTF8.GetBytes(Configuration["TokenConfigurations:JWT_Secret"].ToString());
            var Issuer = Configuration["TokenConfigurations:Issuer"].ToString();
            var Audience = Configuration["TokenConfigurations:Audience"].ToString();

            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Issuer,
                        ValidAudience = Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }

        // esquema de autentica��o
        public void EsquemaDeAutenticacao(IApplicationBuilder app)
        {
            app.UseAuthentication();
        }
    }
}
