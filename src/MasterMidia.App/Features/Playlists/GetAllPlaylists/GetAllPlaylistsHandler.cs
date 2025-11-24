using System;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Playlists.GetAllPlaylists;

public class GetAllPlaylistsHandler(IPlaylistRepository playlistRepository)
{
    private readonly IPlaylistRepository _playlistRepository = playlistRepository;

    public async Task<List<GetAllPlaylistsResponse>> Handle()
    {
        var playlists = await _playlistRepository.GetAllPlaylists();

        var response = playlists
            .Select(playlist => new GetAllPlaylistsResponse(
                playlist.Id,
                playlist.Name,
                playlist.UserId,
                playlist.Contents.Count
            ))
            .ToList();

        return response;
    }
}
