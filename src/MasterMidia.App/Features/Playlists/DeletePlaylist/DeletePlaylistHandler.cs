using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.DeletePlaylist;

public class DeletePlaylistHandler(IPlaylistRepository repository)
{
    private readonly IPlaylistRepository _repository = repository;

    public async Task Handle(DeletePlaylistRequest request, Guid userId)
    {
        var playlist = await _repository.GetPlaylistById(request.PlaylistId) ?? throw new NotFoundException($"Playlist com ID '{request.PlaylistId}' não foi encontrada.");

        if (playlist.UserId != userId)
        {
            throw new ForbiddenException("Acesso negado. Você não é o proprietário desta playlist.");
        }

        await _repository.DeletePlaylist(request.PlaylistId);
    }
}
