using System;

namespace MasterMidia.App.Features.Playlists.UpdatePlaylist;

public record UpdatePlaylistRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}