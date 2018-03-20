using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using senai.spacekids.repository.Context;
using Microsoft.EntityFrameworkCore;
using senai.spacekids.domain.Contracts;
using senai.spacekids.repository.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authorization;
using senai.spacekids.domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace senai.spacekids.webapi
{
    public class Startup
    {

        public IConfiguration Configuration{get;}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                builder => builder.AllowAnyOrigin());
            });

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(authOptions => { 
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions => {
                var parametrosValidacao = bearerOptions.TokenValidationParameters;
                parametrosValidacao.IssuerSigningKey = signingConfigurations.Key;
                parametrosValidacao.ValidAudience = tokenConfigurations.Audience;
                parametrosValidacao.ValidIssuer = tokenConfigurations.Issuer;
                parametrosValidacao.ValidateIssuerSigningKey = true;
            });

            services.AddAuthorization(auth => {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });
            
            services.AddMvc();

            services.AddSwaggerGen( c => 
            {
                c.SwaggerDoc("v1", new Info 
                {
                    Version = "v1",
                    Title = "Forum Api",
                    Description = "Test Simples",
                    TermsOfService = "None",
                    Contact = new Contact 
                    {Name = "Cristiane Maciel, Henrique da Mata", Email = "cristiane.pachecoreis@gmail.com, henrique.damata.novai@gmail.com"}
                });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine (basePath, "spacekids.xml");

            c.IncludeXmlComments(xmlPath);

            });



            services.AddDbContext<SpaceKidsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SpaceKids")));

            services.AddMvc().AddJsonOptions(options => {
                 options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             });;

             services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();
                
            app.UseCors("AllowAnyOrigin");
            
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1");
            });

            
        }
    }
}
