using System;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Contents.GetContentById;

[Tags("Gerenciamento de Conteúdo")]
public class GetContentByIdController(GetContentByIdHandler handler) : ApiControllerBase
{
    // A rota completa será: GET /api/contents/{id}
    /// <summary>
    /// Busca um conteúdo pelo seu identificador único (ID).
    /// </summary>
    /// <remarks>
    /// Retorna os dados completos do conteúdo, se encontrado.
    /// </remarks>
    /// <param name="id">O ID (Guid) do conteúdo a ser buscado.</param>
    [HttpGet("/api/contents/{id}")]
    public async Task<IActionResult> GetContentById(Guid id)
    {
        var response = await handler.Handle(id);
        
        return Ok(response);
    }
}
