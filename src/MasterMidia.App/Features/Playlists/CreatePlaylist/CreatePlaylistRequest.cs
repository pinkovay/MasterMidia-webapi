using System;

namespace MasterMidia.App.Features.Playlists.CreatePlaylist;

public record CreatePlaylistRequest
{
    public string Name { get; init; } = string.Empty;
}
