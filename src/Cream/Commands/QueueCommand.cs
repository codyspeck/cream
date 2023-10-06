using Cream.Common;
using Discord;
using Discord.Commands;
using Victoria.Node;

namespace Cream.Commands;

public class QueueCommand : ModuleBase<SocketCommandContext>
{
    private readonly LavaNode _lavaNode;

    public QueueCommand(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    [Command("queue")]
    [Summary("Display the queue.")]
    public async Task ExecuteAsync()
    {
        if (!_lavaNode.TryGetPlayer(Context.Guild, out var player))
        {
            await ReplyAsync("I'm not currently in a voice channel.");
            return;
        }

        var embed = new EmbedBuilder()
            .WithTitle($"{Context.Guild.Name}'s Queue")
            .WithDescription(player.Vueue.Count == 1
                ? "There is currently 1 song in the queue."
                : $"There are currently {player.Vueue.Count} songs in the queue.")
            .When(player.Vueue.Any(), builder => builder
                .WithFields(new EmbedFieldBuilder()
                    .WithName("Upcoming Songs")
                    .WithValue(string.Join(Environment.NewLine, player.Vueue
                        .Take(10)
                        .Select(lt => $"1. {lt.Title}")))))
            .Build();

        await ReplyAsync(embed: embed);
    }
}