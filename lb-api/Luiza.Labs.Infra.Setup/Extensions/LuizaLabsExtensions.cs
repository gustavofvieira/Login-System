using Luiza.Labs.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Luiza.Labs.Infra.Setup.Extensions
{
    public static class LuizaLabsExtensions
    {
        public static IServiceCollection AddLuizaLabsContext(this IServiceCollection services, IConfiguration config)
        {
            return services
                 .AddScoped(sp =>
                    new LuizaLabsContext(
                        sp.GetRequiredService<MongoClient>().GetDatabase(config.GetConnectionString("LuizaLabsDatabase"))
                    )
                );
        }

        //public static IServiceCollection AddLuizaLabsJwtToken(this IServiceCollection services, IConfiguration config)
        //{
        //    //return services.AddSingleton(config.GetSection("Settings").GetSection("Secret"));
        //    return services.AddOptions<SettingsOptions>(config.GetSection("Settings"));
        //}
    }
}
