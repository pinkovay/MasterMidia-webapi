using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.AddContentToPlaylist;

[Tags("Gerenciamento de Playlists")]
public class AddContentToPlaylistController(AddContentToPlaylistHandler handler) : ApiControllerBase
{
    private readonly AddContentToPlaylistHandler _handler = handler;

    /// <summary>
    /// Adiciona um Conteúdo existente à Playlist especificada.
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer um Token JWT válido. O usuário deve ser o proprietário da Playlist.
    /// </remarks>
    /// <param name="playlistId">O ID da Playlist a ser modificada.</param>
    /// <param name="request">O ID do Conteúdo a ser adicionado.</param>
    [Authorize] 
    [HttpPost("/api/playlists/{playlistId}/contents")]
    public async Task<IActionResult> AddContent([FromRoute] Guid playlistId, [FromBody] AddContentToPlaylistRequest request)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido: ID do usuário ausente ou mal formatado." });
        }

        try
        {
            var response = await _handler.Handle(playlistId, request, userId);

            return StatusCode(201, response); 
        }
        catch (ArgumentException ex) when (ex.Message.Contains("não encontrada") || ex.Message.Contains("não encontrado"))
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return StatusCode(403, new { message = ex.Message });
        }
    }
}