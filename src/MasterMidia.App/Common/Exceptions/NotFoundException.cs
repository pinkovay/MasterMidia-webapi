using System;

namespace MasterMidia.App.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string resourceName, Guid id)
        : base($"O recurso '{resourceName}' com ID '{id}' não foi encontrado.")
    {
    }
}