namespace Application.Dtos.Auth;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public record LoginRequestDto
{
    private string _username = string.Empty;
    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters")]    
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
    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 6 and 20 characters")]
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
