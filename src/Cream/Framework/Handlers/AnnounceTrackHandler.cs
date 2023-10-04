using Cream.Framework.Notifications;
using MediatR;

namespace Cream.Framework.Handlers;

public class AnnounceTrackHandler : INotificationHandler<OnTrackStartNotification>
{
    public async Task Handle(OnTrackStartNotification notification, CancellationToken cancellationToken)
    {
        await notification.Arg.Player.TextChannel.SendMessageAsync($"Now playing: {notification.Arg.Track.Title}");
    }
}