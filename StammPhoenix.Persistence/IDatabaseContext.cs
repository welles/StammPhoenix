using StammPhoenix.Persistence.Models;

namespace StammPhoenix.Persistence;

public interface IDatabaseContext
{
    Task<PlannedEvent[]> GetPlannedEvents();

    Task<T?> GetSetting<T>(string name);

    Task UpdateSetting(string name, object value);

    Task<LoginUser?> FindUserById(string id);

    Task<LoginUser?> FindUserByEmail(string email);

    Task ChangeUserPassword(string id, string passwordHash);

    Task ChangeUserNeedsPasswordChange(string id, bool needsPasswordChange);

    Task CreateUser(LoginUser user);
}