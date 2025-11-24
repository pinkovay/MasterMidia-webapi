using System;

namespace MasterMidia.App.Domain.Entities;

// Individuos em geral que utilizam o sistema.
public class User
{
    public Guid Id {get; private set;}
    public string Username {get; private set;} = string.Empty;
    public string Email {get; private set;} = string.Empty;
    public string? PasswordHash {get; private set;} = string.Empty;
    public ICollection<Playlist> Playlists {get; private set;} = [];
    public ICollection<Content> PostedContent {get; private set;} = [];

    private User(){}

    private User(Guid id, string username, string email, string passwordHash, ICollection<Playlist> playlists, ICollection<Content> postedContent)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Playlists = playlists;
        PostedContent = postedContent;
    }

    public static User Create(string username, string email, string passwordHash) 
        => new(Guid.NewGuid(), username, email, passwordHash, [], []);

    public static User Load(Guid id, string username, string email, ICollection<Playlist> playlists, ICollection<Content> postedContent) 
        => new(id, username, email, string.Empty, playlists, postedContent);

    public void AddPlaylist(Playlist playlist)
    {
        Playlists.Add(playlist);
    }
}
