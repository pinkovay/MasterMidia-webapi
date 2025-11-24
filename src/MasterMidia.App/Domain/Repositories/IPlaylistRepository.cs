using System;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Domain.Repositories;

public interface IPlaylistRepository
{
    Task AddPlaylist(Playlist playlist);
    Task UpdatePlaylist(Playlist playlist);
    Task DeletePlaylist(Guid id);
    Task<List<Playlist>> GetAllPlaylists();
    Task<Playlist?> GetPlaylistById(Guid id);
}
