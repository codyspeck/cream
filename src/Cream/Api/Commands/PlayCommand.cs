using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Lavalink4NET;
using Lavalink4NET.Rest.Entities.Tracks;

namespace Cream.Api.Commands;

public class PlayCommand : BaseCommandModule
{
    private readonly IAudioService _audioService;

    public PlayCommand(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [Command("play")]
    public async Task Execute(CommandContext ctx, [RemainingText] string query)
    {        
        if (!_audioService.Players.TryGetPlayer(ctx.Guild.Id, out var player))
        {
            await ctx.RespondAsync("No connected player found for this server.");
            return;
        }

        var track = await _audioService.Tracks
            .LoadTrackAsync(query, TrackSearchMode.YouTube);

        await player.PlayAsync(track!);
    }
}