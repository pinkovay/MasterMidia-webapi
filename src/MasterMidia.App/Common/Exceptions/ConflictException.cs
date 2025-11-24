using System;

namespace MasterMidia.App.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }

    public ConflictException(string resourceName, string key)
        : base($"{key} indisponivel, tente usar outro.")
    {
    }
}
