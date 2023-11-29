using Lavalink4NET.Players;
using Lavalink4NET.Players.Queued;

namespace Cream.Api.Models;

public record TrackData(TrackReference Reference) : ITrackQueueItem
{
    public ulong ChannelId { get; init; }
    
    public ulong GuildId { get; init; }
}
