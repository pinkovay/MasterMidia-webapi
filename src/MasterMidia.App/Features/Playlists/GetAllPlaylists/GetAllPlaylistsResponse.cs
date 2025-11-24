using System;

namespace MasterMidia.App.Features.Playlists.GetAllPlaylists;

public record GetAllPlaylistsResponse(
    Guid Id,
    string Name,
    Guid UserId,
    int ContentCount
);