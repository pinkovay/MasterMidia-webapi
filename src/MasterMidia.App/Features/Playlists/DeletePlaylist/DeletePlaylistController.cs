using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using MasterMidia.App.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.DeletePlaylist;

[Tags("Gerenciamento de Playlists")]
public class DeletePlaylistController(DeletePlaylistHandler handler) : ApiControllerBase
{
    private readonly DeletePlaylistHandler _handler = handler;

    /// <summary>
    /// Deleta uma Playlist existente pertencente ao usuário autenticado.
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer um Token JWT válido. O ID da Playlist é fornecido na rota.
    /// O ID do usuário autenticado é usado para verificar a propriedade.
    /// </remarks>
    /// <param name="playlistId">O ID da Playlist a ser deletada (fornecido na rota).</param>
    /// <returns>HTTP 204 No Content se a deleção for bem-sucedida.</returns>
    [Authorize]
    [HttpDelete("/api/playlists/{playlistId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid playlistId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido: ID do usuário ausente ou mal formatado." });
        }
        
        var request = new DeletePlaylistRequest(playlistId);

        try
        {
            await _handler.Handle(request, userId);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            // 404 Not Found se a playlist não existir
            return NotFound(new { message = ex.Message });
        }
        catch (ForbiddenException ex)
        {
            // 403 Forbidden se o usuário não for o dono da playlist
            return StatusCode(403, new { message = ex.Message });
        }
    }
}