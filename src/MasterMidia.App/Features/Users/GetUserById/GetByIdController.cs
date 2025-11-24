using System;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Users.GetUserById;

[Tags("Gerenciamento de Usuários")]
public class GetUserByIdController(GetUserByIdHandler handler) : ApiControllerBase
{
    // A rota completa será: GET /api/users/{id}
    /// <summary>
    /// Busca usuário pelo ID fornecido.
    /// </summary>
    /// <remarks>
    /// Retorna os dados completos do usuário, se encontrado.
    /// </remarks>
    /// <param name="id">O ID (Guid) do usuário a ser buscado.</param>
    /// <returns>Dados do usuário ou um erro 404 (Not Found).</returns>
    [HttpGet("/api/users/{id}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var response = await handler.Handle(id);
        
        // 2. Retorna 200 OK com o DTO do usuário
        return Ok(response);
    }
}
