using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity<uint>
{
    private string _username = string.Empty;
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
