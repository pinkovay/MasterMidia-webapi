using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Domain.Entities;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Features.Users.CreateUser;

public class CreateUserHandler(ApplicationDbContext dbContext, IPasswordHashService passwordHashService)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IPasswordHashService _passwordHashService = passwordHashService;

    public async Task<Guid> Handle(CreateUserRequest request)
    {
        var user = User.Create(
            request.Username.ToLowerInvariant(), 
            request.Email.ToLowerInvariant(), 
            _passwordHashService.HashPassword(request.Password)
        );

        _dbContext.Users.Add(user);

        try
        {
            await _dbContext.SaveChangesAsync();
            return user.Id;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is Microsoft.Data.Sqlite.SqliteException sqliteEx && sqliteEx.SqliteErrorCode == 19)
            {
                var normalizedUsername = request.Username.ToLowerInvariant();
                var normalizedEmail = request.Email.ToLowerInvariant();
                
                if (sqliteEx.Message.Contains("UNIQUE constraint failed: Users.Username"))
                {
                    throw new ConflictException("User", $"Username '{normalizedUsername}'");
                }
                if (sqliteEx.Message.Contains("UNIQUE constraint failed: Users.Email"))
                {
                    throw new ConflictException("User", $"Email '{normalizedEmail}'");
                }
            }
            throw; 
        }
    }
}
