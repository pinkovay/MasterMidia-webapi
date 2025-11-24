using System;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Features.Users.GetAllUsers;

public class GetAllUsersHandler(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<List<GetAllUsersResponse>> Handle()
    {
        var users = await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();
            
        var response = users
            .Select(user => new GetAllUsersResponse(
                user.Id,
                user.Username,
                user.Email
            ))
            .ToList();

        return response;
    }
}