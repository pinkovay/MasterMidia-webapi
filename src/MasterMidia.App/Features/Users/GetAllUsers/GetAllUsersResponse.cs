using System;

namespace MasterMidia.App.Features.Users.GetAllUsers;

public record GetAllUsersResponse(
    Guid Id,
    string Username,
    string Email
);
