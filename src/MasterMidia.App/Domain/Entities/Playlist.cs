using System;

namespace MasterMidia.App.Domain.Entities;

public class Playlist
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public User User {get; private set;} = default!;
    public ICollection<Content> Contents {get; private set; } = [];

    private Playlist(){}

    private Playlist(Guid id, string name, Guid userId, ICollection<Content> contents)
    {
        Id = id;
        Name = name;
        UserId = userId;
        Contents = contents;
    }

    public static Playlist Create(string name, Guid userId)
        => new(Guid.NewGuid(), name, userId, []);

    public static Playlist Load(Guid id, string name, Guid userId, ICollection<Content> contents)
        => new(id, name, userId, contents);

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void AddContentToPlaylist(Content content)
    {
        if (Contents.Any(c => c.Id == content.Id))
        {
            throw new InvalidOperationException($"O conteúdo {content.Id} já está na playlist '{Name}'.");
        }
        Contents.Add(content);
    }

    public void RemoveContentFromPlaylist(Guid contentId)
    {
        var contentToRemove = Contents.FirstOrDefault(c => c.Id == contentId) ?? throw new ArgumentException($"O conteúdo {contentId} não está presente na playlist '{Name}'.");

        // 2. Remove o Content da coleção (a remoção do relacionamento será persistida)
        Contents.Remove(contentToRemove);
    }
}

