using System;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.RemoveContentFromPlaylist;

public class RemoveContentFromPlaylistHandler(IPlaylistRepository playlistRepository)
{
    private readonly IPlaylistRepository _playlistRepository = playlistRepository;

    public async Task<RemoveContentFromPlaylistResponse> Handle(Guid playlistId, Guid contentId, Guid userId)
    {
        var playlist = await _playlistRepository.GetPlaylistById(playlistId) ?? throw new ArgumentException($"Playlist com ID {playlistId} não encontrada.", nameof(playlistId));

        // 2. Validação de Autorização (Regra de Negócio)
        if (playlist.UserId != userId)
        {
            throw new UnauthorizedAccessException("Usuário não tem permissão para modificar esta playlist.");
        }

        playlist.RemoveContentFromPlaylist(contentId); 
        
        await _playlistRepository.UpdatePlaylist(playlist);

        return new RemoveContentFromPlaylistResponse
        {
            PlaylistId = playlist.Id,
            ContentId = contentId,
            PlaylistName = playlist.Name
        };
    }
}
