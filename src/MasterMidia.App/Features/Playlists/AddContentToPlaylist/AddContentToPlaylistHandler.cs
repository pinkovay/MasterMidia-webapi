using System;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.AddContentToPlaylist;

public class AddContentToPlaylistHandler(IPlaylistRepository playlistRepository, IContentRepository contentRepository)
{
    private readonly IPlaylistRepository _playlistRepository = playlistRepository;
    private readonly IContentRepository _contentRepository = contentRepository;

    public async Task<AddContentToPlaylistResponse> Handle(Guid playlistId, AddContentToPlaylistRequest request, Guid userId)
    {
        var playlist = await _playlistRepository.GetPlaylistById(playlistId) ?? throw new ArgumentException($"Playlist com ID {playlistId} não encontrada.", nameof(playlistId));

        if (playlist.UserId != userId)
        {
            throw new UnauthorizedAccessException("Usuário não tem permissão para modificar esta playlist.");
        }

        var content = await _contentRepository.GetContentById(request.ContentId) ?? throw new ArgumentException($"Conteúdo com ID {request.ContentId} não encontrado.", nameof(request.ContentId));

        playlist.AddContentToPlaylist(content);
        
        await _playlistRepository.UpdatePlaylist(playlist);

        var newItem = playlist.Contents.First(item => item.Id == request.ContentId);
        
        return new AddContentToPlaylistResponse
        {
            PlaylistId = playlist.Id,
            ContentId = newItem.Id,
        };
    }
}
