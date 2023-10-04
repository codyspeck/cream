using MediatR;
using Victoria.Node.EventArgs;
using Victoria.Player;

namespace Cream.Framework.Notifications;

public class OnTrackStartNotification : INotification
{
    public TrackStartEventArg<LavaPlayer<LavaTrack>, LavaTrack> Arg { get; }

    public OnTrackStartNotification(TrackStartEventArg<LavaPlayer<LavaTrack>, LavaTrack> arg)
    {
        Arg = arg;
    }
}