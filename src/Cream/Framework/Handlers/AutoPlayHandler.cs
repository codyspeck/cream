using Cream.Framework.Notifications;
using MediatR;
using Victoria.Player;

namespace Cream.Framework.Handlers;

public class AutoPlayHandler : INotificationHandler<OnTrackEndNotification>
{
    private readonly ILogger _logger;

    public AutoPlayHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task Handle(OnTrackEndNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Arg.Reason is TrackEndReason.Stopped)
            return;
        
        if (!notification.Arg.Player.Vueue.TryDequeue(out var track))
            return;
        
        await notification.Arg.Player.PlayAsync(track);
    }
}