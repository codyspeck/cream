﻿using Cream.Api.Models;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Lavalink4NET;
using Lavalink4NET.Players;
using Lavalink4NET.Players.Queued;
using Lavalink4NET.Rest.Entities.Tracks;
using Microsoft.Extensions.Options;

namespace Cream.Api.Modules;

public class Play : BaseCommandModule
{
    private readonly IAudioService _audioService;

    public Play(IAudioService audioService)
    {
        _audioService = audioService;
    }

    [Command("play")]
    public async Task Execute(CommandContext ctx, [RemainingText] string query)
    {
        if (!_audioService.Players.TryGetPlayer<IQueuedLavalinkPlayer>(ctx.Guild.Id, out var player) || player is null)
            return;
        
        if (ctx.Member?.VoiceState is null)
            return;
        
        _ = await _audioService.Players.JoinAsync(
            ctx.Guild.Id,
            ctx.Member.VoiceState.Channel.Id,
            PlayerFactory.Queued,
            Options.Create(new QueuedLavalinkPlayerOptions()));

        var track = await _audioService.Tracks
            .LoadTrackAsync(query, TrackSearchMode.YouTube);

        if (track is null)
            return;
        
        var trackData = new TrackData(new TrackReference(track))
        {
            ChannelId = ctx.Channel.Id,
            GuildId = ctx.Guild.Id
        };

        await player.PlayAsync(trackData);
    }
}