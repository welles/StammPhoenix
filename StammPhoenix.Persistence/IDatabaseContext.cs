using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence;

public interface IDatabaseContext
{
    Task<PlannedEvent[]> GetPlannedEvents();

    Task<PageContact[]> GetPageContacts();

    Task<LoginUser?> FindUserById(string id);

    Task<LoginUser?> FindUserByEmail(string email);

    Task ChangeUserPassword(LoginUser user, string passwordHash);

    Task ChangeUserNeedsPasswordChange(LoginUser user, bool needsPasswordChange);

    Task ChangeUserEmail(LoginUser user, string newEmail);

    Task<Guid> CreateUser(LoginUser user);

    Task<bool> VerifyConnection();

    Task Migrate();
}
