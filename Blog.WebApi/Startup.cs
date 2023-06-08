using AutoMapper;
using Blog.Application.Mapper;
using Blog.Application.Services.Contracts;
using Blog.Application.Services.Implementations;
using Blog.Infrastructure;
using Blog.Infrastructure.Contracts;
using Blog.Infrastructure.Implementation;
using Blog.Infrastructure.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi
{
    public class Startup
    {
        /// <summary>
        /// Gets Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        string allowOriginsPolicy = "allowOriginsPolicy";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DbConnection")));
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddControllers();            
            services.AddAutoMapper(typeof(BlogMapper));
            services.AddSwaggerGen(options =>
            {
               options.CustomSchemaIds(type => type.ToString());
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blog API",
                    Description = "An ASP.NET Core Web API for managing Blog items",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Udit X",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Udit Chauhan",
                        Url = new Uri("https://example.com/license")
                    }
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
           });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("SecurityKeys:Token").Value)),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidIssuer = "UserService"
                    };
                });
            var corsOrigin = Configuration.GetSection("AllowedOrigins").Value;
            services.AddCors(options =>
            {
                options.AddPolicy(name: allowOriginsPolicy,
                policy =>
                {
                    policy.WithOrigins(corsOrigin.Split(','));
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == Environments.Development)
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseCors(allowOriginsPolicy);
            /*app.UseCors(
                 policy =>
                     policy.WithOrigins(corsOrigin.Split(','))
                     .AllowAnyMethod()
                     .AllowAnyHeader()
            );*/
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {                
                endpoints.MapControllers();
            });            
        }
    }
}
