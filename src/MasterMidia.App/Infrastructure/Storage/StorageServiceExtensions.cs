using System;
using Google.Cloud.Storage.V1;
using MasterMidia.App.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MasterMidia.App.Infrastructure.Storage
{
    /// <summary>
    /// Extensão para configurar os serviços de Firebase Storage (Google Cloud Storage).
    /// Use services.AddFirebaseStorage() no Program.cs.
    /// </summary>
    public static class StorageServiceExtensions
    {
        public static IServiceCollection AddFirebaseStorage(this IServiceCollection services)
        {
            // 1. Configura o Google.Cloud.Storage.V1.StorageClient como Singleton.
            // O StorageClient é thread-safe e caro para criar, por isso é um Singleton.
            services.AddSingleton(provider =>
            {
                // StorageClient.Create() busca as credenciais no ambiente (GOOGLE_APPLICATION_CREDENTIALS)
                // ou em credenciais de serviço do GCP/Azure, autenticando-se.
                return StorageClient.Create();
            });

            // 2. Registra a implementação do serviço de armazenamento.
            services.AddScoped<IStorageService, FirebaseStorageService>();

            return services;
        }
    }
}
