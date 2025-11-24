using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Features.Users.GetMyUser;

public class GetMyUserHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GetMyUserResponse> Handle(Guid userId)
    {
        // 1. Busca o usuário pelo ID, incluindo a coleção de Playlists
        var user = await _dbContext.Users
            .Include(u => u.Playlists)
            .Include(u => u.PostedContent)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new NotFoundException("User authenticated", userId);

        

        var playlistResponses = user.Playlists
            .OrderBy(p => p.Name)
            .Select(p => new UserPlaylistResponse(
                p.Id,
                p.Name
            ))
            .ToList();

        var postedContent = user.PostedContent
            .OrderBy(pc => pc.Title)
            .Select(pc => new UserPostedContent(
                pc.Id,
                pc.Title,
                pc.StorageUrl,
                pc.MediaType
            ))
            .ToList();

        // 3. Mapeia o User para o DTO de Resposta final
        return new GetMyUserResponse(
            user.Id,
            user.Username,
            user.Email,
            playlistResponses, // Lista de DTOs de Playlist
            postedContent
        );
    }
}
