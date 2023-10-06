using Discord.Commands;
using Victoria.Node;
using Victoria.Player;

namespace Cream.Commands;

public class ResumeCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public ResumeCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("resume")]
    [Summary("Resume queue.")]
    public async Task ExecuteAsync()
    {
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            await ReplyAsync("I'm not currently in a voice channel.");
            return;
        }

        switch (player.PlayerState)
        {
            case PlayerState.Paused:
                await player.ResumeAsync();
                return;
            case PlayerState.Stopped when player.Vueue.TryDequeue(out var track):
                await player.PlayAsync(track);
                break;
        }
    }
}