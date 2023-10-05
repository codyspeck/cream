using Discord;
using MediatR;

namespace Cream.Framework.Notifications;

public class LogNotification : INotification
{
    public LogMessage LogMessage { get; }

    public LogNotification(LogMessage logMessage)
    {
        LogMessage = logMessage;
    }
}