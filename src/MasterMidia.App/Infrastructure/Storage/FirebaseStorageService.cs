using System;
using Google.Cloud.Storage.V1;
using MasterMidia.App.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MasterMidia.App.Infrastructure.Storage;

public class FirebaseStorageService(StorageClient storageClient, IConfiguration configuration) : IStorageService
{
    private readonly StorageClient _storageClient = storageClient;
    private readonly string _bucketName = configuration["Firebase:StorageBucketName"]
            ?? throw new InvalidOperationException("Firebase:StorageBucketName não configurado. Verifique o appsettings.json.");

    public async Task<string> UploadFileAsync(IFormFile file, string destinationPath)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("O arquivo é nulo ou vazio.");
        }

        // Garante que o separador de caminho seja sempre barra (/)
        var objectName = destinationPath.Replace("\\", "/"); 

        try
        {
            // 1. Realiza o Upload usando o método correto da Google Cloud Storage.
            // Argumentos: bucket, nome do objeto (caminho), tipo de conteúdo e o stream de dados.
            await _storageClient.UploadObjectAsync(
                bucket: _bucketName,
                objectName: objectName,
                contentType: file.ContentType,
                source: file.OpenReadStream() // O IFormFile.OpenReadStream() é a fonte (source) dos dados.
            );

            // 2. Retorna a URL pública no formato padrão do Google Cloud Storage
            // O Firebase usa este formato por baixo dos panos.
            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERRO DE STORAGE] Falha ao fazer upload do arquivo {objectName}: {ex.Message}");
            // Re-lança a exceção encapsulada.
            throw new InvalidOperationException("Falha na comunicação com o serviço de Storage. Verifique credenciais e permissões.", ex);
        }
    }
}
