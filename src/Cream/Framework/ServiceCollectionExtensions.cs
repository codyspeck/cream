using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Cream.Framework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDiscordSocketClient(this IServiceCollection services)
    {
        services.AddSingleton(_ =>
        {
            var discordSocketConfig = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            return new DiscordSocketClient(discordSocketConfig);
        });

        return services;
    }
}