using System;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Domain.Repositories;

public interface IContentRepository
{
    Task AddContent(Content content);
    Task UpdateContent(Content content);
    Task<Content?> GetContentById(Guid id);
}
