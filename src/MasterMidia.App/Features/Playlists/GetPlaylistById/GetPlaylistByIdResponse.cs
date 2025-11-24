using System;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Features.Playlists.GetPlaylistById;

public record PlaylistContentResponse(
    Guid Id,
    string Title,
    string Url,
    MediaType MediaType
);

public record GetPlaylistByIdResponse(
    Guid Id,
    string Name,
    Guid UserId,
    string OwnerUsername,
    int ContentCount,
    IEnumerable<PlaylistContentResponse> PlaylistContent
);
