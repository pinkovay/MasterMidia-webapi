using System;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.GetPlaylistById;

[Tags("Gerenciamento de Playlists")]
public class GetPlaylistByIdController(GetPlaylistByIdHandler handler) : ApiControllerBase
{
    // A rota completa será: GET /api/users/{id}
    /// <summary>
    /// Busca uma playlist pelo seu identificador único (ID).
    /// </summary>
    /// <remarks>
    /// Retorna os dados completos da playlist, se encontrado.
    /// </remarks>
    /// <param name="id">O ID (Guid) da playlist a ser buscada.</param>
    /// <returns>Dados do usuário ou um erro 404 (Not Found).</returns>
    [HttpGet("/api/playlists/{id}")]
    public async Task<IActionResult> GetPlaylistById(Guid id)
    {
        var response = await handler.Handle(id);
        
        return Ok(response);
    }
}
