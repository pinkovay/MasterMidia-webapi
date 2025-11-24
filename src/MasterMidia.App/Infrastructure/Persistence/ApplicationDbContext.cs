using System;
using MasterMidia.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterMidia.App.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users {get; set;} = default!;
    public DbSet<Content> Contents {get; set;} = default!;
    public DbSet<Playlist> Playlists {get; set;} = default!;
    public DbSet<Creator> Creators {get; set;} = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Playlist>()
            .HasOne(p => p.User)                             // Uma Playlist tem Um User (Dono)
            .WithMany(u => u.Playlists)                      // Um User tem Muitas Playlists
            .HasForeignKey(p => p.UserId)                    // Chave estrangeira explícita
            .IsRequired();                                   // Não permite Playlist sem Dono
        
        modelBuilder.Entity<Content>()               
            .HasOne(c => c.User)                          // Um Content tem um User
            .WithMany(u => u.PostedContent)               // Um User tem Muitos Posted Contents
            .HasForeignKey(c => c.UserId)                 // Chave estrangeira explícita
            .IsRequired();                                   // Não permite Content sem Criador
        
        modelBuilder.Entity<Playlist>()              
            .HasMany(p => p.Contents)                        // Playlist tem muitos Contents
            .WithMany()                                      // Content não tem uma propriedade de navegação de volta (unidirecional)
            .UsingEntity(j => j.ToTable("PlaylistContent")); // Nomeia a tabela de junção no DB

        modelBuilder.Entity<Content>()
            .Property(c => c.MediaType)
            .HasConversion<string>();                        // Armazena o enum como string
            
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
            
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
    }
}
