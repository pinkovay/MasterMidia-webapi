using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.UpdatePlaylist;

public class UpdatePlaylistHandler(IPlaylistRepository playlistRepository)
{
    private readonly IPlaylistRepository _playlistRepository = playlistRepository;

    public async Task Handle(UpdatePlaylistRequest request, Guid userId)
    {
        var playlist = await _playlistRepository.GetPlaylistById(request.Id) ?? throw new NotFoundException($"Playlist com ID {request.Id} não encontrada.");

        if (playlist.UserId != userId)
        {
            throw new ForbiddenException("Você não tem permissão para editar esta playlist.");
        }

        playlist.UpdateName(request.Name);
        
        await _playlistRepository.UpdatePlaylist(playlist);
    }
}
