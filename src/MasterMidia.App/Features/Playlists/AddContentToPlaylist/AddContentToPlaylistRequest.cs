using System;

namespace MasterMidia.App.Features.Playlists.AddContentToPlaylist;

public record AddContentToPlaylistRequest
{
    public Guid ContentId { get; init; }
}