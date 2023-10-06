using Discord;
using Discord.Commands;
using Victoria.Node;
using Victoria.Player;
using Victoria.Responses.Search;

namespace Cream.Commands;

public class FridayCommand : ModuleBase<SocketCommandContext>
{
    private const string Friday = "https://www.youtube.com/watch?v=iCFOcqsnc9Y&ab_channel=rebecca";

    private readonly LavaNode _lavaNode;

    public FridayCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("friday")]
    [Summary("Play Friday.")]
    public async Task ExecuteAsync(int count)
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
        
        var result = await _lavaNode.SearchAsync(SearchType.Direct, Friday);

        var track = result.Tracks.First();
        
        if (count <= 0)
            count = 1;
        
        for (var i = 0; i < count; i++)
            player.Vueue.Enqueue(track);

        if (count > 1)
            await ReplyAsync($"Queued Friday x{count}");
        
        if (player.PlayerState is PlayerState.Playing or PlayerState.Paused)
            return;
        
        player.Vueue.TryDequeue(out var lavaTrack);

        await player.PlayAsync(lavaTrack);
    }
}