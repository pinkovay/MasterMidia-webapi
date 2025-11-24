using System;

namespace MasterMidia.App.Domain.Entities;

public class Creator
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<Content> Contents {get; private set; } = [];

    private Creator(){}

    private Creator(Guid id, string name, ICollection<Content> contents)
    {
        Id = id;
        Name = name;
        Contents = contents;
    }

    public static Creator Create(string name)
        => new(Guid.NewGuid(), name, []);

    public static Creator Load(Guid id, string name, ICollection<Content> contents)
        => new(id, name, contents);
}
