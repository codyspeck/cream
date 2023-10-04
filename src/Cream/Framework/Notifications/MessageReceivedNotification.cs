using Discord.WebSocket;
using MediatR;

namespace Cream.Framework.Notifications;

public class MessageReceivedNotification : INotification
{
    public SocketMessage SocketMessage { get; }

    public MessageReceivedNotification(SocketMessage socketMessage)
    {
        SocketMessage = socketMessage;
    }
}
