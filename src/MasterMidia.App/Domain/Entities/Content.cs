using System;

namespace MasterMidia.App.Domain.Entities;

public enum MediaType
{
    Video,
    Music,
    Podcast
}

public enum ContentStatus
{
    PendingUpload,     // Criado, mas esperando o arquivo ser enviado
    Processing,        // Arquivo enviado, em transcodificação/análise
    Ready,             // Processamento concluído, pronto para ser consumido
    Error,             // Ocorreu um erro no processamento
    TakedownRequested  // Conteúdo marcado para remoção
}

public class Content
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public MediaType MediaType {get; private set;}
    public Guid UserId { get; private set; }
    public User User {get; internal set;} = default!;
    public string StorageUrl { get; private set; } = string.Empty;
    public ContentStatus Status { get; private set; }

    private Content(){}

    private Content(Guid id, string title, Guid userId, MediaType mediaType, ContentStatus status)
    {
        Id = id;
        Title = title;
        UserId = userId;
        MediaType = mediaType;
        Status = status;
    }

    public static Content Create(string title, Guid userId, MediaType mediaType)
        => new(Guid.NewGuid(), title, userId, mediaType, ContentStatus.PendingUpload);

    public static Content Load(Guid id, string title, Guid userId, MediaType mediaType, ContentStatus status)
        => new(id, title, userId, mediaType, status);

    public void UpdateStorageDetails(string storageUrl)
    {
        if (string.IsNullOrWhiteSpace(storageUrl))
        {
            throw new ArgumentException("Storage URL não pode ser nula ou vazia.", nameof(storageUrl));
        }

        StorageUrl = storageUrl;
        UpdateStatus(ContentStatus.Processing);
    }

    public void UpdateStatus(ContentStatus newStatus)
    {
        Status = newStatus;
    }

    public void MarkAsReady()
    {
        Status = ContentStatus.Ready;
    }
}