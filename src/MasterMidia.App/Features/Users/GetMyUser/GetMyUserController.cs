using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using MasterMidia.App.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Users.GetMyUser;

[Tags("Gerenciamento de Usuários")]
public class GetMyUserController(GetMyUserHandler handler) : ApiControllerBase
{
    private readonly GetMyUserHandler _handler = handler;

    // A rota completa será: GET /api/users/me
    /// <summary>
    /// Busca todos os dados do usuário logado.
    /// </summary>
    /// <remarks>
    /// Retorna o perfil completo do usuário, incluindo todas as suas playlists.
    /// </remarks>
    /// <returns>Dados completos do usuário (200 OK).</returns>
    [Authorize]
    [HttpGet("/api/users/me")]
    public async Task<IActionResult> GetMyUserAsync()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido: ID do usuário ausente ou mal formatado." });
        }

        try
        {
            var response = await _handler.Handle(userId);
            return Ok(response);
        } catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
