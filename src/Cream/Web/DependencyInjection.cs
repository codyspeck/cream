using DSharpPlus;
using Lavalink4NET.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cream.Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        var discordConfiguration = configuration
            .GetSection(DiscordConfiguration.ConfigurationKey)
            .Get<DiscordConfiguration>();

        var lavalinkConfiguration = configuration
            .GetSection(LavalinkConfiguration.ConfigurationKey)
            .Get<LavalinkConfiguration>();
        
        var dspConfiguration = new DSharpPlus.DiscordConfiguration
        {
            Token = discordConfiguration.Token,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        };
        
        services.AddSingleton(new DiscordClient(dspConfiguration));
        
        services.AddLavalink();

        services.ConfigureLavalink(options =>
        {
            options.BaseAddress = lavalinkConfiguration.Host;
            options.Passphrase = lavalinkConfiguration.Password;
        });

        services.AddHostedService<BotAudioService>();
        services.AddHostedService<BotService>();
        
        return services;
    }
}