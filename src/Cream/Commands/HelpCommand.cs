using Cream.Common;
using Discord;
using Discord.Commands;

namespace Cream.Commands;

public class HelpCommand : ModuleBase<SocketCommandContext>
{
    private readonly CommandService _commandService;

    public HelpCommand(CommandService commandService)
    {
        _commandService = commandService;
    }

    [Command("help")]
    [Summary("Display a list of available commands.")]
    public async Task ExecuteAsync()
    {
        var embed = new EmbedBuilder()
            .AddFields(_commandService.Commands, m => m.Name, m => m.Summary ?? "-")
            .Build();

        await ReplyAsync(embed: embed);
    }
}