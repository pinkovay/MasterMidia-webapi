using System.Runtime.Intrinsics.X86;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Features.Users.GetMyUser;

public record UserPlaylistResponse(
    Guid Id,
    string Name
);

public record UserPostedContent(
    Guid Id,
    string Title,
    string Url,
    MediaType MediaType
);

public record GetMyUserResponse(
    Guid Id,
    string Username,
    string Email,
    IEnumerable<UserPlaylistResponse> Playlists,
    IEnumerable<UserPostedContent> PostedContent

);
