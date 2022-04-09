using StammPhoenix.Util.Models;

namespace StammPhoenix.Util.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);

    PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
}
