using Cream.Api.Models;
using DSharpPlus;
using Lavalink4NET;
using Lavalink4NET.Players.Queued;
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
        _audioService.TrackStarted += async (_, args) =>
        {
            var player = args.Player as IQueuedLavalinkPlayer;

            var track = player!.CurrentItem as TrackData;
            
            var channel = await _client.GetChannelAsync(track!.ChannelId);
            
            await _client.SendMessageAsync(channel, $"Now playing: {args.Track.Uri}");
        };

        return Task.CompletedTask;
    }
}
