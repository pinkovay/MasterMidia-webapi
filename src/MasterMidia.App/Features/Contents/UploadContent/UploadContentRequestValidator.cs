using System;
using FluentValidation;
using MasterMidia.App.Domain.Entities;

namespace MasterMidia.App.Features.Contents.UploadContent;

public class UploadContentRequestValidator : AbstractValidator<UploadContentRequest>
{
    public UploadContentRequestValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("O arquivo de mídia é obrigatório.")
            .Must(f => f != null && f.Length > 0).WithMessage("O arquivo não pode ser vazio.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título do conteúdo é obrigatório.")
            .Length(5, 200).WithMessage("O título deve ter entre 5 e 200 caracteres.");

        RuleFor(x => x.MediaType)
            .IsInEnum().WithMessage("Tipo de mídia inválido.")
            .Must(type => Enum.IsDefined(typeof(MediaType), type))
            .WithMessage($"Tipo de mídia deve ser Video (0), Music (1) ou Podcast (2).");
    }
}
