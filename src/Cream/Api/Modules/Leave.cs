using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Lavalink4NET;

namespace Cream.Api.Modules;

public class Leave : BaseCommandModule
{
    private readonly IAudioService _audioService;

    public Leave(IAudioService audioService)
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