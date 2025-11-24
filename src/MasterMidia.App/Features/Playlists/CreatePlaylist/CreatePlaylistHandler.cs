using System;
using MasterMidia.App.Domain.Entities;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.CreatePlaylist;

public class CreatePlaylistHandler(IPlaylistRepository repository)
{
    private readonly IPlaylistRepository _repository = repository;

    public async Task<CreatePlaylistResponse> Handle(CreatePlaylistRequest request, Guid userId)
    {
        var playlist = Playlist.Create(request.Name, userId);

        await _repository.AddPlaylist(playlist);

        return new CreatePlaylistResponse
        {
            Id = playlist.Id,
            Name = playlist.Name,
            UserId = playlist.UserId
        };
    }
}
