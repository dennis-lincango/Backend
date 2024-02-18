using Application.Interfaces.Services;

namespace Infrastructure.Services.Hash;

public class BcryptHashService : IHashService
{
    public string Hash(string text)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(text);
    }

    public bool Verify(string text, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(text, hash);
    }
}
