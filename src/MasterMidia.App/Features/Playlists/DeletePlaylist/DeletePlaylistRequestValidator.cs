using System;
using FluentValidation;

namespace MasterMidia.App.Features.Playlists.DeletePlaylist;

public class DeletePlaylistRequestValidator : AbstractValidator<DeletePlaylistRequest>
{
    public DeletePlaylistRequestValidator()
    {
        RuleFor(x => x.PlaylistId)
            .NotEmpty().WithMessage("O ID da playlist é obrigatório para exclusão.");
    }
}