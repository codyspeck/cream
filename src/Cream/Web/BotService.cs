using System.Reflection;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Lavalink4NET;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Cream.Web;

public class BotService : BackgroundService
{
    private readonly DiscordClient _discordClient;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _services;

    public BotService(DiscordClient discordClient, IConfiguration configuration, IServiceProvider services)
    {
        _discordClient = discordClient;
        _configuration = configuration;
        _services = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var discordConfiguration = _configuration
            .GetSection(DiscordConfiguration.ConfigurationKey)
            .Get<DiscordConfiguration>();
        
        var commands = _discordClient.UseCommandsNext(new CommandsNextConfiguration
        {
            StringPrefixes = new[] { discordConfiguration.Prefix },
            Services = _services
        });
        
        commands.RegisterCommands(Assembly.GetExecutingAssembly());
        
        await _discordClient.ConnectAsync();
    }
}