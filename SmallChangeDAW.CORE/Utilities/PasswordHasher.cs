using System.Security.Cryptography;
using System.Text;

namespace SmallChangeDAW.CORE.Utilities;

/// <summary>
/// Utilidad para hash y validación de contraseñas usando PBKDF2.
/// Proporciona métodos seguros para almacenar y validar contraseñas en la base de datos.
/// </summary>
public static class PasswordHasher
{
    private const int KeySize = 64; // 512 bits
    private const int Iterations = 350000; // PBKDF2 iterations (NIST recommendation 2024)
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    /// <summary>
    /// Genera un hash seguro para una contraseña.
    /// Retorna un string en formato Base64 con salt incluido.
    /// </summary>
    public static string HashPassword(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(
            password,
            salt: new byte[16],
            iterations: Iterations,
            hashAlgorithm: Algorithm))
        {
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            // Formato: iterations:salt:key
            return $"{Iterations}:{salt}:{key}";
        }
    }

    /// <summary>
    /// Verifica si una contraseña coincide con un hash existente.
    /// </summary>
    public static bool VerifyPassword(string password, string hash)
    {
        try
        {
            var parts = hash.Split(':');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out var iterations))
                return false;

            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                Algorithm))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);
                return keyToCheck.SequenceEqual(key);
            }
        }
        catch
        {
            return false;
        }
    }
}
