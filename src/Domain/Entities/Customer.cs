using Domain.Common;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities;

public class Customer : BaseEntity<uint>
{
    private string _firstName = string.Empty;
    [Required(ErrorMessage = "First name is required")]
    [StringLength(10, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 10 characters")]
    [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "First name can only contain letters and spaces")]
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
    [Required(ErrorMessage = "Last name is required")]
    [StringLength(10, MinimumLength = 5, ErrorMessage = "Last name must be between 5 and 10 characters")]
    [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Last name can only contain letters and spaces")]
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
    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(10, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 10 digits")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Phone number can only contain digits")]
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
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    [StringLength(30, ErrorMessage = "Email must be at most 30 characters")]
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
    [Required(ErrorMessage = "Address is required")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 30 characters")]
    [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Address can only contain letters, numbers, and spaces")]
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
