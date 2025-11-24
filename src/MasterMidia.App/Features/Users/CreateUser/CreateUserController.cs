using MasterMidia.App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Users.CreateUser;

[Tags("Gerenciamento de Usuários")]
public class CreateUserController(CreateUserHandler createUserHandler) : ApiControllerBase
{
    /// <summary>
    /// Cria um novo usuário
    /// </summary>
    /// <remarks>
    /// Este endpoint registra um novo usuário e retorna o link para o recurso criado (Location header).
    /// </remarks>
    /// <param name="request">Dados do usuário a ser criado (username, email, password).</param>
    /// <returns>O ID do usuário recém-criado e a URL para acesso ao recurso.</returns>
    [HttpPost("/api/users")]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var userId = await createUserHandler.Handle(request);
        
        return CreatedAtAction(
            actionName: "GetUserById", 
            controllerName: "GetUserById",
            routeValues: new { id = userId }, 
            value: new { id = userId }
        );
    }
}
