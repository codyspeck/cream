using Cream.Framework.Notifications;
using Discord.Commands;
using Discord.WebSocket;
using MediatR;

namespace Cream.Framework.Handlers;

public class TextCommandHandler : INotificationHandler<MessageReceivedNotification>
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _service;
    private readonly IServiceProvider _provider;
    private readonly ILogger _logger;

    public TextCommandHandler(DiscordSocketClient client, CommandService service, IServiceProvider provider, ILogger logger)
    {
        _client = client;
        _service = service;
        _provider = provider;
        _logger = logger;
    }

    public async Task Handle(MessageReceivedNotification request, CancellationToken cancellationToken)
    {   
        if (request.SocketMessage is not SocketUserMessage message)
            return;

        var position = 0;

        if (!message.HasCharPrefix('!', ref position))
            return;

        _logger.Debug("Receiving message from {Author}.", request.SocketMessage.Author.Username);
        
        var context = new SocketCommandContext(_client, message);

        await _service.ExecuteAsync(context, position, _provider);
    }
}