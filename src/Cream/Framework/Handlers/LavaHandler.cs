using Cream.Framework.Notifications;
using MediatR;
using Victoria.Node;

namespace Cream.Framework.Handlers;

public class LavaHandler : INotificationHandler<ReadyNotification>
{
    private readonly LavaNode _lavaNode;
    private readonly ILogger _logger;

    public LavaHandler(LavaNode lavaNode, ILogger logger)
    {
        _lavaNode = lavaNode;
        _logger = logger;
    }

    public async Task Handle(ReadyNotification notification, CancellationToken cancellationToken)
    {
        _logger.Information("Connecting to Lavalink");
        
        await _lavaNode.ConnectAsync();
    }
}