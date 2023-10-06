using Cream.Framework.Notifications;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Victoria.Node;

namespace Cream.Framework;

public static class ServiceProviderExtensions
{
    public static IHost RegisterDiscordSocketClientEvents(this IHost host)
    {
        var client = host.Services.GetRequiredService<DiscordSocketClient>();

        client.Log += async message =>
        {
            await using var scope = host.Services.CreateAsyncScope();

            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new LogNotification(message));
        };
        
        client.MessageReceived += async message =>
        {
            await using var scope = host.Services.CreateAsyncScope();
            
            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new MessageReceivedNotification(message));
        };
        
        client.Ready += async () =>
        {
            await using var scope = host.Services.CreateAsyncScope();
            
            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new ReadyNotification());
        };

        client.SelectMenuExecuted += async component =>
        {
            await using var scope = host.Services.CreateAsyncScope();

            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new SelectMenuExecutedNotification(component));
        };

        return host;
    }

    public static IHost RegisterLavaNodeEvents(this IHost host)
    {
        var node = host.Services.GetRequiredService<LavaNode>();

        node.OnTrackEnd += async arg =>
        {
            await using var scope = host.Services.CreateAsyncScope();
            
            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new OnTrackEndNotification(arg));
        };

        node.OnTrackStart += async arg =>
        {
            await using var scope = host.Services.CreateAsyncScope();

            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new OnTrackStartNotification(arg));
        };

        return host;
    }
}