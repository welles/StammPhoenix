using StammPhoenix.Persistence.Constants;
using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence;

public interface IDatabaseContext
{
    Task<PlannedEvent[]> GetPlannedEvents();

    Task<PageContact[]> GetPageContacts();

    Task<Team[]> GetTeams();

    Task<LoginUser?> FindUserById(Guid id);

    Task<LoginUser?> FindUserByEmail(string email);

    Task ChangeUserPassword(LoginUser user, string passwordHash);

    Task ChangeUserNeedsPasswordChange(LoginUser user, bool needsPasswordChange);

    Task ChangeUserEmail(LoginUser user, string newEmail);

    Task ChangeUserSecurityStamp(LoginUser user, Guid newSecurityStamp);

    Task<bool> VerifyUserSecurityStamp(Guid userId, Guid securityStamp);

    Task<Guid> CreateUser(LoginUser user);

    Task<Team?> FindTeamForRank(Rank rank);

    Task<bool> VerifyConnection();

    Task Migrate();
}
