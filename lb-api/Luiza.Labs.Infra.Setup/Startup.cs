using Luiza.Labs.Domain.Interfaces.Repositories;
using Luiza.Labs.Domain.Interfaces.Services;
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

            services
                .AddMongoClientConfiguration(Configuration)
                .AddLuizaLabsContext(Configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }
}