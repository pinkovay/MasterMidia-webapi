using System;
using MasterMidia.App.Domain.Entities;
using MasterMidia.App.Domain.Repositories;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Infrastructure.Repositories;

public class PlaylistRepository(ApplicationDbContext dbContext) : IPlaylistRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddPlaylist(Playlist playlist)
    {
        _dbContext.Playlists.Add(playlist);
        await _dbContext.SaveChangesAsync();
    }

    public Task UpdatePlaylist(Playlist playlist)
    {
        _dbContext.Playlists.Update(playlist);
        return _dbContext.SaveChangesAsync();
    }

    public async Task DeletePlaylist(Guid id)
    {
        var playlist = await _dbContext.Playlists.FindAsync(id);
        if (playlist != null)
        {
            _dbContext.Playlists.Remove(playlist);
            await _dbContext.SaveChangesAsync();
        }
    }

    public Task<List<Playlist>> GetAllPlaylists()
    {
        return _dbContext.Playlists
            .Include(p => p.Contents)
            .ToListAsync();
    }

    public Task<Playlist?> GetPlaylistById(Guid id)
    {
        return _dbContext.Playlists
            .Include(p => p.User)
            .Include(p => p.Contents)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
   
