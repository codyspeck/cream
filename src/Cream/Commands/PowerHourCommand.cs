using Cream.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Victoria.Node;
using Victoria.Responses.Search;

namespace Cream.Commands;

public class PowerHourCommand : ModuleBase<SocketCommandContext>
{
    private const long PowerHourChannelId = 835689615335620658;

    private readonly DiscordSocketClient _client;
    private readonly LavaNode _lavaNode;

    public PowerHourCommand(DiscordSocketClient client, LavaNode lavaNode)
    {
        _client = client;
        _lavaNode = lavaNode;
    }

    [Command("ph")]
    [Summary("Play a power hour.")]
    public async Task ExecuteAsync()
    {
        if (_client.GetChannel(PowerHourChannelId) is not SocketTextChannel channel)
        {
            await ReplyAsync("Couldn't find the Power Hour channel. You may be running this command on the wrong Discord server.");
            return;
        }

        var messages = await channel
            .GetMessagesAsync()
            .ToListAsync();

        var responses = await Task
            .WhenAll(messages
                .SelectMany(m => m)
                .Where(m => Uri.TryCreate(m.Content, UriKind.Absolute, out _))
                .Select(m => _lavaNode.SearchAsync(SearchType.Direct, m.Content))
                .ToList());

        var tracks = responses
            .SelectMany(r => r.Tracks)
            .ToList();

        var component = new ComponentBuilder()
            .WithSelectMenu(new SelectMenuBuilder()
                .WithCustomId(Constants.PowerHourSelectMenuId)
                .AddOptions(tracks, t => (t.Title, t.Url)))
            .Build();

        var embed = new EmbedBuilder()
            .WithDescription(string.Join(Environment.NewLine, tracks.Select(t => t.Title)))
            .Build();

        await ReplyAsync(embed: embed, components: component);
    }
}