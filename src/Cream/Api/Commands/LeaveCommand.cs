using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Lavalink4NET;

namespace Cream.Api.Commands;

public class LeaveCommand : BaseCommandModule
{
    private readonly IAudioService _audioService;

    public LeaveCommand(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [Command("leave")]
    public async Task Execute(CommandContext ctx)
    {
        if (!_audioService.Players.TryGetPlayer(ctx.Guild.Id, out var player))
            return;
        
        await player.DisconnectAsync();
    }
}