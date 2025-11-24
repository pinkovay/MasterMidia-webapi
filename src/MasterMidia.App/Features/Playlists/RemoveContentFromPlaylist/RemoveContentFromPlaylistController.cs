using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.RemoveContentFromPlaylist;

[Tags("Gerenciamento de Playlists")]
public class RemoveContentFromPlaylistController(RemoveContentFromPlaylistHandler handler) : ApiControllerBase
{
    private readonly RemoveContentFromPlaylistHandler _handler = handler;

    /// <summary>
    /// Remove um Conteúdo específico de uma Playlist do usuário.
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer autenticação. O usuário deve ser o proprietário da Playlist.
    /// </remarks>
    /// <param name="playlistId">O ID da Playlist a ser modificada.</param>
    /// <param name="contentId">O ID do Conteúdo a ser removido.</param>
    /// <returns>Confirmação da remoção.</returns>
    [Authorize] 
    [HttpDelete("/api/playlists/{playlistId}/contents/{contentId}")]
    public async Task<IActionResult> RemoveContent(
        [FromRoute] Guid playlistId, 
        [FromRoute] Guid contentId)
    {
        // 1. Tenta extrair o ID do usuário autenticado
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido ou ID do usuário ausente." });
        }

        if (playlistId == Guid.Empty || contentId == Guid.Empty)
        {
            return BadRequest(new { message = "Os IDs da Playlist e do Conteúdo são obrigatórios." });
        }

        try
        {
            var response = await _handler.Handle(playlistId, contentId, userId);

            return Ok(response); 
        }
        catch (ArgumentException ex) // Captura 404: Playlist não encontrada ou Conteúdo não na Playlist
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex) // Captura 403: Usuário não autorizado a mexer na playlist
        {
            return StatusCode(403, new { message = ex.Message });
        }
    }
}
