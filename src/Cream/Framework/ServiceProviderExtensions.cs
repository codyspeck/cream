using Cream.Framework.Notifications;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Victoria.Node;

namespace Cream.Framework;

public static class ServiceProviderExtensions
{
    public static IServiceProvider RegisterDiscordSocketClientEvents(this IServiceProvider provider)
    {
        var client = provider.GetRequiredService<DiscordSocketClient>();

        client.Log += async message =>
        {
            await using var scope = provider.CreateAsyncScope();

            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new LogNotification(message));
        };
        
        client.MessageReceived += async message =>
        {
            await using var scope = provider.CreateAsyncScope();
            
            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new MessageReceivedNotification(message));
        };
        
        client.Ready += async () =>
        {
            await using var scope = provider.CreateAsyncScope();
            
            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new ReadyNotification());
        };
        
        return provider;
    }

    public static IServiceProvider RegisterLavaNodeEvents(this IServiceProvider provider)
    {
        var node = provider.GetRequiredService<LavaNode>();
        
        node.OnTrackEnd += async arg =>
        {
            await using var scope = provider.CreateAsyncScope();
            
            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new OnTrackEndNotification(arg));
        };

        node.OnTrackStart += async arg =>
        {
            await using var scope = provider.CreateAsyncScope();

            await scope.ServiceProvider
                .GetRequiredService<IMediator>()
                .Publish(new OnTrackStartNotification(arg));
        };

        return provider;
    }
}