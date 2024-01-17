namespace Application.Dtos.Customers;

public record CreateCustomerDto
{
    private string _firstName = string.Empty;
    public string FirstName
    {
        get
        {
            return _firstName;
        }
        init
        {
            _firstName = value;
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get
        {
            return _lastName;
        }
        init
        {
            _lastName = value;
        }
    }

    private string _phone = string.Empty;
    public string Phone
    {
        get
        {
            return _phone;
        }
        init
        {
            _phone = value;
        }
    }

    private string _email = string.Empty;
    public string Email
    {
        get
        {
            return _email;
        }
        init
        {
            _email = value;
        }
    }

    private string _address = string.Empty;
    public string Address
    {
        get
        {
            return _address;
        }
        init
        {
            _address = value;
        }
    }
}
