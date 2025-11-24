using System;

namespace MasterMidia.App.Features.Playlists.RemoveContentFromPlaylist;

public record RemoveContentFromPlaylistResponse
{
    public Guid PlaylistId { get; init; }
    public Guid ContentId { get; init; }
    public string PlaylistName { get; init; } = string.Empty;
}
