using System;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Playlists.GetAllPlaylists;

[Tags("Gerenciamento de Playlists")]
public class GetAllPlaylistsController(GetAllPlaylistsHandler handler) : ApiControllerBase
{
    // A rota completa será: GET /api/playlists
    /// <summary>
    /// Busca a lista completa de todas as Playlists registradas no sistema.
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer um Token JWT válido. Retorna um array contendo os DTOs de todas as playlists.
    /// Esta query é aberta para todos os usuários autenticados, retornando playlists públicas.
    /// </remarks>
    /// <returns>Lista de playlists (200 OK).</returns>
    [Authorize]
    [HttpGet("/api/playlists")]
    public async Task<IActionResult> GetAllPlaylistsAsync()
    {
        var response = await handler.Handle();
        
        return Ok(response);
    }
}