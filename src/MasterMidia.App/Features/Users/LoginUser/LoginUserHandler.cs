using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Features.Users.LoginUser;

public class LoginUserHandler(
    ApplicationDbContext dbContext,
    IPasswordHashService passwordHashService,
    ITokenService tokenService)
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IPasswordHashService _passwordHashService = passwordHashService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<LoginUserResponse> Handle(LoginUserRequest request)
    {
        var normalizedEmail = request.Email.ToLowerInvariant();

        
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == normalizedEmail);

        if (user == null || user.PasswordHash == null ||
            !_passwordHashService.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedException("Credenciais inválidas.");
        }

        // 4. Gera o Token JWT
        var token = _tokenService.GenerateToken(user.Id);

        return new LoginUserResponse(token);
    }
}
