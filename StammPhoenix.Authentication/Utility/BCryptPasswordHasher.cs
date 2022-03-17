using Microsoft.AspNetCore.Identity;

namespace StammPhoenix.Authentication.Utility;

public class BCryptPasswordHasher<TUser> : IPasswordHasher<TUser> where TUser : class
{
    private const int WorkLoad = 12;

    public string HashPassword(TUser user, string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkLoad);
    }

    public PasswordVerificationResult VerifyHashedPassword(
        TUser user, string hashedPassword, string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, WorkLoad))
        {
            return PasswordVerificationResult.SuccessRehashNeeded;
        }

        return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}