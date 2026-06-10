namespace ProductOrderAPI.Infrastructure.Services;

public class UserRecord
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public static class UserStore
{
    private static readonly List<UserRecord> _users = new();

    public static void Add(UserRecord user) => _users.Add(user);

    public static UserRecord? FindByUsername(string username) =>
        _users.FirstOrDefault(u => u.Username == username);
}