using FluentValidation;
using FluentValidation.AspNetCore;
using MasterMidia.App.Domain.Repositories;
using MasterMidia.App.Features.Contents.GetContentById;
using MasterMidia.App.Features.Contents.UploadContent;
using MasterMidia.App.Features.Playlists.AddContentToPlaylist;
using MasterMidia.App.Features.Playlists.CreatePlaylist;
using MasterMidia.App.Features.Playlists.DeletePlaylist;
using MasterMidia.App.Features.Playlists.GetAllPlaylists;
using MasterMidia.App.Features.Playlists.GetPlaylistById;
using MasterMidia.App.Features.Playlists.RemoveContentFromPlaylist;
using MasterMidia.App.Features.Playlists.UpdatePlaylist;
using MasterMidia.App.Features.Users;
using MasterMidia.App.Features.Users.CreateUser;
using MasterMidia.App.Features.Users.GetAllUsers;
using MasterMidia.App.Features.Users.GetMyUser;
using MasterMidia.App.Features.Users.GetUserById;
using MasterMidia.App.Features.Users.LoginUser;
using MasterMidia.App.Infrastructure.Auth;
using MasterMidia.App.Infrastructure.Persistence;
using MasterMidia.App.Infrastructure.Repositories;
using MasterMidia.App.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MasterMidia.App.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            
        });

        // Validators
        services.AddFluentValidationAutoValidation();

        // Extensions
        services.AddJwtAuthentication(configuration);

        // Handler
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<GetUserByIdHandler>();
        services.AddScoped<GetAllUsersHandler>();
        services.AddScoped<LoginUserHandler>();
        services.AddScoped<CreatePlaylistHandler>();
        services.AddScoped<UpdatePlaylistHandler>();
        services.AddScoped<DeletePlaylistHandler>();
        services.AddScoped<GetPlaylistByIdHandler>();
        services.AddScoped<GetAllPlaylistsHandler>();
        services.AddScoped<GetMyUserHandler>();
        services.AddScoped<UploadContentHandler>();
        services.AddScoped<AddContentToPlaylistHandler>();
        services.AddScoped<GetContentByIdHandler>();
        services.AddScoped<RemoveContentFromPlaylistHandler>();
        
        // Services
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<ITokenService, TokenService>();
        
        // Repositories
        services.AddScoped<IPlaylistRepository, PlaylistRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();

        return services;
    }
}
