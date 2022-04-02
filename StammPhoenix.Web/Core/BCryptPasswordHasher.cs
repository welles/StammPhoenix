namespace StammPhoenix.Web.Core;

public class BCryptPasswordHasher : IPasswordHasher
{
    //private const int WorkLoad = 12;
    private const string Salt = "$2a$12$/v4WQkKf4MH8AUma.6eLnO";

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCryptPasswordHasher.Salt);
    }

    public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        var isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

        // if (isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, WorkLoad))
        // {
        //     return PasswordVerificationResult.SuccessRehashNeeded;
        // }

        return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
    }
}
