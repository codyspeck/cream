using Cream.Framework.Notifications;
using Discord;
using MediatR;

namespace Cream.Framework.Handlers;

public class AnnounceTrackHandler : INotificationHandler<OnTrackStartNotification>
{
    public async Task Handle(OnTrackStartNotification notification, CancellationToken cancellationToken)
    {
        var embed = new EmbedBuilder()
            .WithDescription($"Playing: {notification.Arg.Track.Title}")
            .Build();
        
        await notification.Arg.Player.TextChannel.SendMessageAsync(embed: embed);
    }
}