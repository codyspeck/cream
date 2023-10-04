using Discord;
using Discord.Commands;
using Victoria.Node;

namespace Cream.Commands;

public class JoinCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public JoinCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("join")]
    public async Task ExecuteAsync()
    {
        // bot is already in a voice channel
        if (_lavaNode.TryGetPlayer(Context.Guild, out _))
        {
            await ReplyAsync("I'm already in a voice channel.");
            return;
        }
    
        // user is not in a voice channel
        if (Context.User is not IVoiceState voiceState)
        {
            await ReplyAsync("You must be connected to a voice channel.");
            return;
        }

        // join the user's voice channel
        await _lavaNode.JoinAsync(voiceState.VoiceChannel, Context.Channel as ITextChannel);
    }
}