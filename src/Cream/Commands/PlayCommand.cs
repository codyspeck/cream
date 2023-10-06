using Discord;
using Discord.Commands;
using Victoria.Node;
using Victoria.Player;
using Victoria.Responses.Search;

namespace Cream.Commands;

public class PlayCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public PlayCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("play")]
    [Summary("Play a song.")]
    public async Task ExecuteAsync([Remainder] string query)
    {
        // bot is not in a voice channel
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            // user is not in a voice channel
            if (Context.User is not IVoiceState voiceState)
            {
                await ReplyAsync("You must be connected to a voice channel.");
                return;
            }

            // join the user's voice channel
            player = await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);
        }

        // search YouTube
        var searchResponse = Uri.IsWellFormedUriString(query, UriKind.Absolute)
            ? await _lavaNode.SearchAsync(SearchType.Direct, query)
            : await _lavaNode.SearchAsync(SearchType.YouTube, query);

        // no search results found
        if (searchResponse.Status is SearchStatus.LoadFailed or SearchStatus.NoMatches)
        {
            await ReplyAsync("No search results found.");
            return;
        }

        // enqueue first result
        var track = searchResponse.Tracks.First();
        
        player.Vueue.Enqueue(track);

        // player is playing or paused, leave track queued
        if (player.PlayerState is PlayerState.Playing or PlayerState.Paused)
        {
            await ReplyAsync($"Queued: {track.Title}");
            return;
        }

        // player is idle, play track
        player.Vueue.TryDequeue(out var lavaTrack);
        
        await player.PlayAsync(lavaTrack);
    }
}