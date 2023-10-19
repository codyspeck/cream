using DSharpPlus;
using Lavalink4NET;
using Microsoft.Extensions.Hosting;

namespace Cream.Api.Services;

public class BotAudioService : BackgroundService
{
    private readonly IAudioService _audioService;
    private readonly DiscordClient _client;

    public BotAudioService(IAudioService audioService, DiscordClient client)
    {
        _audioService = audioService;
        _client = client;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _audioService.TrackStarted += async (sender, args) =>
        {
            var channel = await _client.GetChannelAsync(args.Player.VoiceChannelId);
            
            await _client.SendMessageAsync(channel, $"Now playing: {args.Track.Uri}");
        };

        return Task.CompletedTask;
    }
}