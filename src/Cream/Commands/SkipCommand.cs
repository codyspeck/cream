using Discord.Commands;
using Victoria.Node;

namespace Cream.Commands;

public class SkipCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public SkipCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("skip")]
    [Summary("Skip the current song.")]
    public async Task ExecuteAsync()
    {
        // bot is not currently in a voice channel
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            await ReplyAsync("I'm not currently in a voice channel.");
            return;
        }

        // last song in the queue, don't skip
        if (!player.Vueue.Any())
        {
            await ReplyAsync("This is the last song in the queue.");
            return;
        }
        
        // skip
        await player.SkipAsync();
    }
}