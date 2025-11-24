using System;
using MasterMidia.App.Features.Users;
namespace MasterMidia.App.Infrastructure.Services;

public class PasswordHashService : IPasswordHashService
{
    /// <summary>
    /// Gera o hash de uma senha utilizando o algoritmo BCrypt.
    /// O BCrypt é a opção recomendada por ser resistente a ataques de força bruta e usar saltwork.
    /// </summary>
    /// <param name="password">A senha em texto claro.</param>
    /// <returns>O hash seguro da senha.</returns>
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }

    /// <summary>
    /// Verifica uma senha em texto claro contra um hash previamente gerado.
    /// </summary>
    /// <param name="password">A senha em texto claro fornecida pelo usuário.</param>
    /// <param name="hash">O hash armazenado no banco de dados.</param>
    /// <returns>True se a senha corresponder ao hash, False caso contrário.</returns>
    public bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
