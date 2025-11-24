using System;
using MasterMidia.App.Common.Exceptions;
using MasterMidia.App.Domain.Repositories;

namespace MasterMidia.App.Features.Contents.GetContentById;

public class GetContentByIdHandler(IContentRepository contentRepository)
{
    private readonly IContentRepository _repository = contentRepository;

    public async Task<GetContentByIdResponse> Handle(Guid id)
    {
        var content = await _repository.GetContentById(id) ?? throw new NotFoundException("Content", id);

        return new GetContentByIdResponse(
            content.Id,
            content.Title,
            content.MediaType,
            content.UserId,
            content.User.Username,
            content.StorageUrl,
            content.Status
        );
    }
}
