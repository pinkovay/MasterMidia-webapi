using System;

namespace MasterMidia.App.Features.Playlists.AddContentToPlaylist;

public record AddContentToPlaylistResponse
{
    public Guid PlaylistId { get; init; }
    public Guid ContentId { get; init; }
}
