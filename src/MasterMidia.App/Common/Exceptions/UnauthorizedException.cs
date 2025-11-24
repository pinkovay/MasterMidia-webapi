using System;

namespace MasterMidia.App.Common.Exceptions;

public class UnauthorizedException(string message) : Exception(message)
{
}
