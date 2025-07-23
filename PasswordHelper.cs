using System;
using System.Security.Cryptography;

/// <summary>
/// Provides methods to securely hash and verify passwords using PBKDF2 with SHA-256.
/// </summary>
public static class PasswordHelper
{
    // Size of the salt in bytes (128 bits)
    private const int SaltSize = 16;
    // Size of the hash in bytes (256 bits)
    private const int HashSize = 32;
    // Number of PBKDF2 iterations (tunable for security vs. performance)
    private const int Iterations = 10000;

    /// <summary>
    /// Generates a cryptographic salt and computes the PBKDF2 hash for the given plaintext password.
    /// </summary>
    /// <param name="password">Plaintext password to hash.</param>
    /// <param name="hashHex">Hex-encoded hash output.</param>
    /// <param name="saltHex">Hex-encoded salt used for hashing.</param>
    public static void CreateHash(string password, out string hashHex, out string saltHex)
    {
        // Generate a secure random salt
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Derive the hash using PBKDF2-SHA256
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(HashSize);
            // Convert to hex strings for storage
            hashHex = BitConverter.ToString(hash).Replace("-", "");
            saltHex = BitConverter.ToString(salt).Replace("-", "");
        }
    }

    /// <summary>
    /// Verifies a plaintext password against a stored hash and salt.
    /// </summary>
    /// <param name="password">Plaintext password to verify.</param>
    /// <param name="storedHashHex">Hex-encoded stored hash.</param>
    /// <param name="storedSaltHex">Hex-encoded stored salt.</param>
    /// <returns>True if the password matches the stored hash; false otherwise.</returns>
    public static bool VerifyPassword(string password, string storedHashHex, string storedSaltHex)
    {
        // Convert hex salt back to byte array
        byte[] salt = HexStringToBytes(storedSaltHex);

        // Recompute the hash with the same parameters
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(HashSize);
            // Compare hex-encoded hashes in constant time
            string hashHex = BitConverter.ToString(hash).Replace("-", "");
            return string.Equals(hashHex, storedHashHex, StringComparison.Ordinal);
        }
    }

    /// <summary>
    /// Converts a hexadecimal string to its corresponding byte array.
    /// </summary>
    /// <param name="hex">Hex string where each byte is represented by two hex characters.</param>
    /// <returns>Byte array decoded from the hex string.</returns>
    private static byte[] HexStringToBytes(string hex)
    {
        int length = hex.Length / 2;
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; i++)
        {
            // Parse two hex characters to one byte
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return bytes;
    }
}