using System;
using FluentValidation;

namespace MasterMidia.App.Features.Playlists.AddContentToPlaylist;

public class AddContentToPlaylistRequestValidator : AbstractValidator<AddContentToPlaylistRequest>
{
    public AddContentToPlaylistRequestValidator()
    {
        RuleFor(x => x.ContentId)
            .NotEmpty().WithMessage("O ID do Conteúdo é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("O ID do Conteúdo deve ser um GUID válido.");
    }
}
