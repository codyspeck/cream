using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace Cream.Framework;

public class DiscordBotBackgroundService : BackgroundService
{
    private readonly DiscordSocketClient _client;
    private readonly CreamConfiguration _configuration;

    public DiscordBotBackgroundService(DiscordSocketClient client, CreamConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _client.LoginAsync(TokenType.Bot, _configuration.Token);
        await _client.StartAsync();
    }
}