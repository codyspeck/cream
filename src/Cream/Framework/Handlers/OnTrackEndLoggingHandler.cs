using Cream.Framework.Notifications;
using MediatR;

namespace Cream.Framework.Handlers;

public class OnTrackEndLoggingHandler : INotificationHandler<OnTrackEndNotification>
{
    private readonly ILogger _logger;

    public OnTrackEndLoggingHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task Handle(OnTrackEndNotification notification, CancellationToken cancellationToken)
    {
        _logger.Debug("Track {Title} ended for reason {Reason}",
            notification.Arg.Track.Title,
            notification.Arg.Reason);
        
        return Task.CompletedTask;
    }
}