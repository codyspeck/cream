using System.Reflection;
using Cream.Framework.Notifications;
using Discord.Commands;
using MediatR;

namespace Cream.Framework.Handlers;

public class CommandHandler : INotificationHandler<ReadyNotification>
{
    private readonly CommandService _commandService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public CommandHandler(CommandService commandService, IServiceProvider serviceProvider, ILogger logger)
    {
        _commandService = commandService;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Handle(ReadyNotification notification, CancellationToken cancellationToken)
    {
        _logger.Information("Initializing CommandService");
        
        await _commandService.AddModulesAsync(Assembly.GetExecutingAssembly(), _serviceProvider);
    }
}