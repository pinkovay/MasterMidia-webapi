using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Features.Users.GetUserById;

public class GetUserByIdHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<GetUserByIdResponse> Handle(Guid id)
    {
        var user = await _dbContext.Users
            .Include(p => p.Playlists)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException("User", id);

        return new GetUserByIdResponse(
            user.Id,
            user.Username,
            user.Email
        );
    }
}
