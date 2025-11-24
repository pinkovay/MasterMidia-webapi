using System;

namespace MasterMidia.App.Common.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("Acesso não autorizado para esta operação.")
    {
    }

    public ForbiddenException(string message)
        : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
