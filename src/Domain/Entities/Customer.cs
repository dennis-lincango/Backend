using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity<uint>
{
    private string _firstName = string.Empty;
    public string FirstName
    {
        get
        {
            return _firstName;
        }
        set
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
        set
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
        set
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
        set
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
        set
        {
            _address = value;
        }
    }

    private List<Shipment> _shipments = [];
    public IReadOnlyCollection<Shipment> Shipments
    {
        get
        {
            return _shipments.AsReadOnly();
        }
        private set
        {
            _shipments = [.. value];
        }
    }
}
