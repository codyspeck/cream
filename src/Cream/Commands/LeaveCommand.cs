using Discord.Commands;
using Victoria.Node;

namespace Cream.Commands;

public class LeaveCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public LeaveCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("leave")]
    [Summary("Leave the active voice channel.")]
    public async Task ExecuteAsync()
    {
        // bot is not currently in a voice channel
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            await ReplyAsync("I'm not currently in a voice channel.");
            return;
        }

        await _lavaNode.LeaveAsync(player.VoiceChannel);
    }
}