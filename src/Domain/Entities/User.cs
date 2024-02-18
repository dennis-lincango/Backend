using Domain.Common;
using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class User : BaseEntity<uint>
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
        set
        {
            _username = value;
        }
    }

    private string _password = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    [StringLength(72, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 72 characters")]
    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            _password = value;
        }
    }

    private UserType _userType = UserType.Operational;
    public UserType UserType
    {
        get
        {
            return _userType;
        }
        init
        {
            _userType = value;
        }
    }

    private uint _loginFailedAttempts = 0;
    public uint LoginFailedAttempts
    {
        get
        {
            return _loginFailedAttempts;
        }
        set
        {
            _loginFailedAttempts = value;
        }
    }

    private bool _isLocked = false;
    public bool IsLocked
    {
        get
        {
            return _isLocked;
        }
        set
        {
            _isLocked = value;
        }
    }
}
