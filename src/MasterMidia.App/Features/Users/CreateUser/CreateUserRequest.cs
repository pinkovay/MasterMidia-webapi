using System;

namespace MasterMidia.App.Features.Users.CreateUser;

public record CreateUserRequest(string Username, string Email, string Password){}
