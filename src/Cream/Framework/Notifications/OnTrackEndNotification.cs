using MediatR;
using Victoria.Node.EventArgs;
using Victoria.Player;

namespace Cream.Framework.Notifications;

public class OnTrackEndNotification : INotification
{
    public TrackEndEventArg<LavaPlayer<LavaTrack>, LavaTrack> Arg { get; }

    public OnTrackEndNotification(TrackEndEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
    {
        Arg = arg;
    }
}