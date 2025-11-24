using System;
using FluentValidation;

namespace MasterMidia.App.Features.Playlists.UpdatePlaylist;

public class UpdatePlaylistRequestValidator : AbstractValidator<UpdatePlaylistRequest>
{
    public UpdatePlaylistRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID da playlist é obrigatório.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome da playlist é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
    }
}