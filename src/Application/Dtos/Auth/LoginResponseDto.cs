namespace Application.Dtos.Auth;

public record LoginResponseDto
{
    private string _token = string.Empty;
    public string Token
    {
        get
        {
            return _token;
        }
        init
        {
            _token = value;
        }
    }
}
