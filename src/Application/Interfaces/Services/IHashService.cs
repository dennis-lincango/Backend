namespace Application.Interfaces.Services;

public interface IHashService
{
    public string Hash(string text);
    public bool Verify(string text, string hash);
}
