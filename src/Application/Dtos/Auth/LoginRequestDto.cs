namespace Application.Dtos.Auth;

public record LoginRequestDto
{
    private string _username = string.Empty;
    public string Username
    {
        get
        {
            return _username;
        }
        init
        {
            _username = value;
        }
    }

    private string _password = string.Empty;
    public string Password
    {
        get
        {
            return _password;
        }
        init
        {
            _password = value;
        }
    }
}
