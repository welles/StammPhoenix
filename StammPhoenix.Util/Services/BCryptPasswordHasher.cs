using StammPhoenix.Util.Interfaces;
using StammPhoenix.Util.Models;

namespace StammPhoenix.Util.Services;

public class BCryptPasswordHasher : IPasswordHasher
{
    private const int WorkLoad = 12;

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCryptPasswordHasher.WorkLoad);
    }

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, BCryptPasswordHasher.WorkLoad))
        {
            return PasswordVerificationResult.SuccessRehashNeeded;
        }

        return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}
