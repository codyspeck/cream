using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Lavalink4NET;
using Lavalink4NET.Players;
using Lavalink4NET.Players.Queued;
using Microsoft.Extensions.Options;

namespace Cream.Api.Modules;

public class Join : BaseCommandModule
{
    private readonly IAudioService _audioService;

    public Join(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [Command("join")]
    public async Task Execute(CommandContext ctx)
    {
        if (ctx.Member?.VoiceState is null)
            return;
        
        _ = await _audioService.Players.JoinAsync(
            ctx.Guild.Id,
            ctx.Member.VoiceState.Channel.Id,
            PlayerFactory.Queued,
            Options.Create(new QueuedLavalinkPlayerOptions()));
    }
}