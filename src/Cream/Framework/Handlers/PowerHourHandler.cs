using Cream.Common;
using Cream.Framework.Notifications;
using Discord;
using Discord.WebSocket;
using MediatR;
using Victoria.Node;
using Victoria.Player;
using Victoria.Responses.Search;

namespace Cream.Framework.Handlers;

public class PowerHourHandler : INotificationHandler<SelectMenuExecutedNotification>
{
    private readonly LavaNode _lavaNode;

    public PowerHourHandler(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    public async Task Handle(SelectMenuExecutedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Component.Data.CustomId is not Constants.PowerHourSelectMenuId)
            return;
        
        await notification.Component.UpdateAsync(c =>
        {
            c.Content = $"Power Hour Selected [{MentionUtils.MentionUser(notification.Component.User.Id)}]";
            c.Components = null;
            c.Embeds = null;
        });

        if (notification.Component.User is not IVoiceState voiceState)
        {
            await notification.Component.RespondAsync("You must be connected to a voice channel.");
            return;
        }

        if (notification.Component.Channel is not SocketGuildChannel channel)
        {
            await notification.Component.RespondAsync("I was expecting this channel to be a socket guild channel.");
            return;
        }

        var response = await _lavaNode.SearchAsync(SearchType.Direct, notification.Component.Data.Values.First());

        if (!_lavaNode.TryGetPlayer(channel.Guild, out var player))
            await _lavaNode.JoinAsync(voiceState.VoiceChannel);
        
        player.Vueue.Enqueue(response.Tracks.First());

        if (player.PlayerState is PlayerState.Playing or PlayerState.Paused)
            return;
        
        player.Vueue.TryDequeue(out var lavaTrack);
        
        await player.PlayAsync(lavaTrack);
    }
}