using System;
using MasterMidia.App.Common;
using MasterMidia.App.Domain.Entities;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Contents.UploadContent;

public class UploadContentHandler(IContentRepository contentRepository, IStorageService storageService)
{
    private readonly IContentRepository _contentRepository = contentRepository;
    private readonly IStorageService _storageService = storageService;

    public async Task<UploadContentResponse> Handle(UploadContentRequest request, Guid userId)
    {
        var content = Content.Create(request.Title, userId, request.MediaType);

        var storagePath = $"content/{userId}/{content.Id}/{request.File.FileName}";

        try
        {
            var fileUrl = await _storageService.UploadFileAsync(request.File, storagePath);
            
            content.UpdateStorageDetails(fileUrl);
            content.MarkAsReady();

            await _contentRepository.AddContent(content);
            
            return new UploadContentResponse
            {
                ContentId = content.Id,
                Title = content.Title,
                StatusMessage = $"Upload aceito. Arquivo salvo em: {fileUrl}"
            };
        }
        catch (Exception ex)
        {
            // Logar o erro (ex: falha de comunicação com Firebase)
            Console.WriteLine($"Erro no Upload para o Storage: {ex.Message}");
            // Em um cenário real, lançaria uma exceção de domínio ou retornaria um resultado de erro.
            throw new InvalidOperationException("Falha ao salvar o arquivo de mídia. Tente novamente mais tarde.", ex);
        }
    }
}
