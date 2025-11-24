using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using MasterMidia.App.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.UpdatePlaylist;

[Tags("Gerenciamento de Playlists")]
public class UpdatePlaylistController(UpdatePlaylistHandler handler) : ApiControllerBase
{
    private readonly UpdatePlaylistHandler _handler = handler;

    /// <summary>
    /// Atualiza o nome de uma Playlist existente pertencente ao usuário autenticado.
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer um Token JWT válido. O ID da Playlist é fornecido no corpo da requisição.
    /// O ID do usuário autenticado é usado para verificar a propriedade.
    /// </remarks>
    /// <param name="request">O ID e o novo nome da Playlist.</param>
    /// <returns>HTTP 204 No Content se a atualização for bem-sucedida.</returns>
    [Authorize]
    [HttpPut("/api/playlists")]
    public async Task<IActionResult> Update([FromBody] UpdatePlaylistRequest request)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido: ID do usuário ausente ou mal formatado." });
        }

        try
        {
            await _handler.Handle(request, userId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ForbiddenException ex)
        {
            return StatusCode(403, new { message = ex.Message });
        }
    }
}
