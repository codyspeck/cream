using Discord.WebSocket;
using MediatR;

namespace Cream.Framework.Notifications;

public class SelectMenuExecutedNotification : INotification
{
    public SocketMessageComponent Component { get; }

    public SelectMenuExecutedNotification(SocketMessageComponent component)
    {
        Component = component;
    }
}