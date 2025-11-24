using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.GetPlaylistById;

public class GetPlaylistByIdHandler(IPlaylistRepository repository)
{
    private readonly IPlaylistRepository _repository = repository;

    public async Task<GetPlaylistByIdResponse> Handle(Guid id)
    {
        var playlist = await _repository.GetPlaylistById(id) ?? throw new NotFoundException("Playlist", id);

        var playlistContentResponse = playlist.Contents
            .OrderBy(pc => pc.Title)
            .Select(pc => new PlaylistContentResponse(
               pc.Id,
                pc.Title,
                pc.StorageUrl,
                pc.MediaType
            ))
            .ToList();

        return new GetPlaylistByIdResponse(
            playlist.Id,
            playlist.Name,
            playlist.UserId,
            playlist.User.Username,
            playlist.Contents.Count,
            playlistContentResponse
        );
    }
}

