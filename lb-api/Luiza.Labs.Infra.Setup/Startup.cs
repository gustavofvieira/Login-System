using FluentValidation;
using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Validations;
using Luiza.Labs.Infra.Data.Repositories;
using Luiza.Labs.Infra.Setup.Extensions;
using Luiza.Labs.Sevices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;

namespace Luiza.Labs.Infra.Setup
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureApp(IServiceCollection services)
        {
            ConfigureServices(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);

            services
                .AddMongoClientConfiguration(Configuration)
                .AddLuizaLabsContext(Configuration);
                //.AddLuizaLabsJwtToken(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public void ConfigureValidators(IServiceCollection services) 
        {
            //services.AddSingleton<HashAlgorithm>();
            services.AddScoped<IValidator<User>, UserValidator>();

        }
    }
}