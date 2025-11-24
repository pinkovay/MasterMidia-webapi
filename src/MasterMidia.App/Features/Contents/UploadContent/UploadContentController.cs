using System;
using System.Security.Claims;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterMidia.App.Features.Contents.UploadContent;

[Tags("Gerenciamento de Conteúdo")]
public class UploadContentController(UploadContentHandler handler) : ApiControllerBase
{
    private readonly UploadContentHandler _handler = handler;

    /// <summary>
    /// Inicia o upload de um arquivo de mídia (vídeo, música ou podcast).
    /// </summary>
    /// <remarks>
    /// **PROTEGIDA:** Requer um Token JWT válido. O ID do Criador é extraído da claim 'NameIdentifier' do token.
    /// <br/><br/>
    /// **IMPORTANTE:** Este endpoint espera dados do tipo **multipart/form-data** e não JSON.
    /// O upload é tratado de forma **assíncrona**, e a resposta será um **202 Accepted**.
    /// </remarks>
    /// <param name="request">O arquivo de mídia (IFormFile), o título e o tipo de mídia.</param>
    /// <returns>Retorna um status 202 Accepted e o ID do conteúdo criado, indicando que o processamento foi iniciado.</returns>
    [Authorize]
    [HttpPost("/api/contents/upload")]
    [Consumes("multipart/form-data")] // CRÍTICO: Indica ao ASP.NET Core para ler o corpo como formulário
    public async Task<IActionResult> UploadContent([FromForm] UploadContentRequest request) // CRÍTICO: Usa [FromForm]
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new { message = "Token inválido: ID do Criador ausente ou mal formatado." });
        }

        var response = await _handler.Handle(request, userId);

        return StatusCode(StatusCodes.Status202Accepted, response);
    }
}