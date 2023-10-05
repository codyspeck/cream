using Cream.Framework.Notifications;
using Discord;
using MediatR;
using Serilog.Events;

namespace Cream.Framework.Handlers;

public class LogHandler : INotificationHandler<LogNotification>
{
    private readonly ILogger _logger;

    public LogHandler(ILogger logger)
    {
        _logger = logger;
    }

    public Task Handle(LogNotification notification, CancellationToken cancellationToken)
    {
        var level = notification.LogMessage.Severity switch
        {
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Critical => LogEventLevel.Fatal
        };
        
        _logger.Write(level, notification.LogMessage.Exception, $"${notification.LogMessage.Source}: ${notification.LogMessage.Message}");
        
        return Task.CompletedTask;
    }
}