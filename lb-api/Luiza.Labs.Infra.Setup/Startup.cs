using FluentValidation;
using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Luiza.Labs.Domain.Validations;
using Luiza.Labs.Infra.Data.Repositories;
using Luiza.Labs.Infra.Setup.Extensions;
using Luiza.Labs.Sevices.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IEmailService, EmailService>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private void ConfigureValidators(IServiceCollection services) 
        {
            services.AddScoped<IValidator<User>, UserValidator>();

        }
    }
}