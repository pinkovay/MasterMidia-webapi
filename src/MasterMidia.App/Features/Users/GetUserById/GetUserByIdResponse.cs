using System;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Features.Users.GetUserById;

/// <summary>
/// DTO de resposta para a busca de um usuário.
/// Contém as informações públicas do usuário.
/// </summary>
public record GetUserByIdResponse(
    Guid Id,
    string Username,
    string Email
);