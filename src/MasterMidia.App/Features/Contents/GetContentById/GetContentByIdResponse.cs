using System;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Features.Contents.GetContentById;

public record GetContentByIdResponse(
    Guid Id,
    string Title,
    MediaType MediaType,
    Guid CreatorId,
    string CreatorUsername,
    string StorageUrl,
    ContentStatus Status
);
    

