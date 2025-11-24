using System;

namespace MasterMidia.App.Features.Users;

public interface ITokenService
{
    string GenerateToken(Guid userId);
}
