using System;

namespace MasterMidia.App.Features.Contents.UploadContent;

public record UploadContentResponse
{
    public Guid ContentId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string StatusMessage { get; init; } = string.Empty;
}
