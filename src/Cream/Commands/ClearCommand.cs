using Discord.Commands;
using Victoria.Node;

namespace Cream.Commands;

public class ClearCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public ClearCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("clear")]
    [Summary("Clear the queue.")]
    public async Task ExecuteAsync()
    {
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            await ReplyAsync("I'm not currently in a voice channel.");
            return;
        }

        player.Vueue.Clear();
    }
}