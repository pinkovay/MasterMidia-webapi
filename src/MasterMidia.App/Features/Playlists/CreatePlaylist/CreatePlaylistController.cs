using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.CreatePlaylist;

[Tags("Gerenciamento de Playlists")]
public class CreatePlaylistController(CreatePlaylistHandler handler) : ApiControllerBase
{
    private readonly CreatePlaylistHandler _handler = handler;

    /// <summary>
    /// Cria uma nova Playlist para o usuário autenticado.
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer um Token JWT válido. O ID do usuário é extraído da claim 'NameIdentifier' do token.
    /// </remarks>
    /// <param name="request">Nome da Playlist.</param>
    /// <returns>A Playlist criada com o seu ID.</returns>
    [Authorize] 
    [HttpPost("/api/playlists")]
    public async Task<IActionResult> Create([FromBody] CreatePlaylistRequest request)
    {
        // 1. Extrai o ID do usuário do Token JWT
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido: ID do usuário ausente ou mal formatado." });
        }

        var response = await _handler.Handle(request, userId);

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }
}