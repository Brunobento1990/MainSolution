using static BCrypt.Net.BCrypt;

namespace Application.Adapters;

public static class PasswordAdapter
{
    public static bool VerifyPassword(string? confirmPassword, string? password)
    {
        if (string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(password))
        {
            return false;
        }

        try
        {
            return Verify(confirmPassword, password);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            return false;
        }
    }

    public static string GenerateHash(string password)
    {
        return HashPassword(password, 10);
    }
}
