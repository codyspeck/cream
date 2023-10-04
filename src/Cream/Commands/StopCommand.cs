using Discord.Commands;
using Victoria.Node;

namespace Cream.Commands;

public class StopCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public StopCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("stop")]
    public async Task ExecuteAsync()
    {
        // bot is not currently in a voice channel
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            await ReplyAsync("I'm not currently in a voice channel.");
            return;
        }
        
        await player.StopAsync();
    }
}