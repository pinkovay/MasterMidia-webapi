using System;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Users.GetAllUsers;

[Tags("Gerenciamento de Usuários")]
public class GetAllUsersController(GetAllUsersHandler handler) : ApiControllerBase
{
    // A rota completa será: GET /api/users
    /// <summary>
    /// Lista todos os usuários
    /// </summary>
    /// <remarks>
    ///  **PROTEGIDA:** Requer um Token JWT válido. Retorna um array contendo os DTOs de todos os usuários.
    /// </remarks>
    [Authorize]
    [HttpGet("/api/users")]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var response = await handler.Handle();
        
        return Ok(response);
    }
}


