using System;

namespace MasterMidia.App.Features.Playlists.CreatePlaylist;

public record CreatePlaylistResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid UserId { get; init; }
}
