#r "nuget: Konscious.Security.Cryptography.Argon2, 1.3.1"
using Konscious.Security.Cryptography;

var passwordHasher = new DefaultPasswordHasher();
var time = DateTime.Now;
passwordHasher.GetPasswordHash("system").Dump("System Password: ");
var afterTime = DateTime.Now;
Console.WriteLine($"Cost time: {afterTime - time}");
passwordHasher.GetPasswordHash("123456").Dump("Admin Password: ");
passwordHasher.GetPasswordHash("operator").Dump("Operator Password: ");

/// <summary>
/// Password hash and the salt used by hash
/// </summary>
/// <param name="password"></param>
/// <param name="salt"></param>
public class PasswordWithSalt
{
    /// <summary>
    /// New
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    public PasswordWithSalt(string password, string salt)
    {
        Password = password;
        Salt = salt;
    }

    /// <summary>
    /// Password hash value
    /// </summary>
    public string Password { get; }
    /// <summary>
    /// Hash Salt
    /// </summary>
    public string Salt { get; }
}

/// <summary>
/// Provide method to translate password to hash value
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hash password
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    string GetPasswordHash(string password, string salt);
    /// <summary>
    /// Hash password and auto set Salt
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    PasswordWithSalt GetPasswordHash(string password);
    /// <summary>
    /// Generate Salt
    /// </summary>
    /// <returns></returns>
    string GenerateSalt();
    /// <summary>
    /// Verify password
    /// </summary>
    /// <param name="passwordHash"></param>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    bool VerifyPassword(string passwordHash, string password, string salt);
}

class DefaultPasswordHasher : IPasswordHasher
{
    public string GenerateSalt()
    {
        return Guid.NewGuid().ToString();
    }

    private Argon2 BuildArgon(string password, string salt)
    {
        var argon2 = new Argon2d(Encoding.UTF8.GetBytes(password));
        argon2.Salt = Encoding.UTF8.GetBytes(salt);
        argon2.DegreeOfParallelism = 4;
        argon2.Iterations = 40;
        argon2.MemorySize = 8192; // 8mb

        return argon2;
    }

    private string GeneratePasswordHash(string password, string salt)
    {
        var argon2 = BuildArgon(password, salt);

        return System.Convert.ToHexString(argon2.GetBytes(128)); // get bytes of length 128
    }

    /// <inheritdoc />
    public string GetPasswordHash(string password, string salt)
    {
        return GeneratePasswordHash(password, salt);
    }

    /// <inheritdoc />
    public PasswordWithSalt GetPasswordHash(string password)
    {
        var salt = GenerateSalt();
        return new PasswordWithSalt(GeneratePasswordHash(password, salt), salt);
    }

    /// <inheritdoc />
    public bool VerifyPassword(string passwordHash, string password, string salt)
    {
        var comparePasswordHash = GeneratePasswordHash(password, salt);
        return passwordHash == comparePasswordHash;
    }
}