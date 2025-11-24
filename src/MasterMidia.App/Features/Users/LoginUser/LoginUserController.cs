using System;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Users.LoginUser;

[Tags("Gerenciamento de Usuários")]
public class LoginUserController(LoginUserHandler handler) : ApiControllerBase
{
    // A rota completa será: POST /api/users/login
    /// <summary>
    /// Realiza o login do usário, validando credenciais e emitindo um Token Jwt.
    /// </summary>
    /// <remarks>
    /// Em caso de sucesso, retorna o token JWT no corpo da resposta (200 OK).
    /// Em caso de falha de autenticação (credenciais inválidas), retorna 401 Unauthorized.
    /// </remarks>
    /// <param name="request">Email e senha do usuário.</param>
    /// <returns>O Token JWT.</returns>
    [HttpPost("/api/users/login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var response = await handler.Handle(request);
        
        return Ok(response);
    }
}
