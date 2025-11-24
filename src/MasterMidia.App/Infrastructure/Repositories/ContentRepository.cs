using System;
using MasterMidia.App.Domain.Entities;
using MasterMidia.App.Domain.Repositories;
using MasterMidia.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Infrastructure.Repositories;

public class ContentRepository(ApplicationDbContext dbContext) : IContentRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task AddContent(Content content)
    {
        _dbContext.Contents.Add(content);

        await _dbContext.SaveChangesAsync();
    }

    public Task<Content?> GetContentById(Guid id)
    {
        return _dbContext.Contents
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateContent(Content content)
    {
        _dbContext.Contents.Update(content);

        await _dbContext.SaveChangesAsync();
    }
}
