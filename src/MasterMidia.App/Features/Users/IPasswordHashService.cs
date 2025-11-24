using System;

namespace MasterMidia.App.Features.Users;

/// <summary>
/// Define a interface para serviços de hashing e verificação de senhas.
/// </summary>
public interface IPasswordHashService
{
    /// <summary>
    /// Gera o hash de uma senha em texto claro.
    /// </summary>
    /// <param name="password">A senha a ser hasheada.</param>
    /// <returns>O hash seguro da senha.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifica se uma senha em texto claro corresponde a um hash armazenado.
    /// </summary>
    /// <param name="password">A senha em texto claro.</param>
    /// <param name="hash">O hash armazenado.</param>
    /// <returns>True se a senha for válida, False caso contrário.</returns>
    bool VerifyPassword(string password, string hash);
}
