using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Lavalink4NET;
using Lavalink4NET.Players;
using Lavalink4NET.Players.Queued;
using Microsoft.Extensions.Options;

namespace Cream.Web.Commands;

public class JoinCommand : BaseCommandModule
{
    private readonly IAudioService _audioService;

    public JoinCommand(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [Command("join")]
    public async Task Execute(CommandContext ctx)
    {
        if (ctx.Member?.VoiceState is null)
        {
            await ctx.RespondAsync("You are not connected to a voice channel.");
            return;
        }
        
        _ = await _audioService.Players.JoinAsync(
            ctx.Guild.Id,
            ctx.Member.VoiceState.Channel.Id,
            PlayerFactory.Queued,
            Options.Create(new QueuedLavalinkPlayerOptions()));
    }
}