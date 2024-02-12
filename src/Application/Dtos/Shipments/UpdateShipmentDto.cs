using Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Application.Dtos.Shipments;

public record UpdateShipmentDto
{
    private string _description = string.Empty;
    [Required(ErrorMessage = "Description is required")]
    [StringLength(70, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 70 characters")]
    public string Description
    {
        get
        {
            return _description;
        }
        init
        {
            _description = value;
        }
    }

    private double _weight = +0.0;
    [Required(ErrorMessage = "Weight is required")]
    [Range(0, 1000, ErrorMessage = "Weight must be between 0 and 1000")]
    public double Weight
    {
        get
        {
            return _weight;
        }
        init
        {
            _weight = value;
        }
    }

    private double _length = +0.0;
    [Required(ErrorMessage = "Length is required")]
    [Range(0, 1000, ErrorMessage = "Length must be between 0 and 1000")]
    public double Length
    {
        get
        {
            return _length;
        }
        init
        {
            _length = value;
        }
    }

    private double _width = +0.0;
    [Required(ErrorMessage = "Width is required")]
    [Range(0, 1000, ErrorMessage = "Width must be between 0 and 1000")]
    public double Width
    {
        get
        {
            return _width;
        }
        init
        {
            _width = value;
        }
    }

    private double _height = +0.0;
    [Required(ErrorMessage = "Height is required")]
    [Range(0, 1000, ErrorMessage = "Height must be between 0 and 1000")]
    public double Height
    {
        get
        {
            return _height;
        }
        init
        {
            _height = value;
        }
    }

    private string _sourceAddress = string.Empty;
    [Required(ErrorMessage = "Source address is required")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Source address must be between 5 and 30 characters")]
    [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Source address can only contain letters, numbers, and spaces")]
    public string SourceAddress
    {
        get
        {
            return _sourceAddress;
        }
        init
        {
            _sourceAddress = value;
        }
    }

    private string _sourceCity = string.Empty;
    [Required(ErrorMessage = "Source city is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Source city must be between 5 and 20 characters")]
    [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Source city can only contain letters and spaces")]
    public string SourceCity
    {
        get
        {
            return _sourceCity;
        }
        init
        {
            _sourceCity = value;
        }
    }

    private string _destinationAddress = string.Empty;
    [Required(ErrorMessage = "Destination address is required")]
    [StringLength(30, MinimumLength = 5, ErrorMessage = "Destination address must be between 5 and 30 characters")]
    [RegularExpression("^[a-zA-Z0-9\\s]+$", ErrorMessage = "Destination address can only contain letters, numbers, and spaces")]
    public string DestinationAddress
    {
        get
        {
            return _destinationAddress;
        }
        init
        {
            _destinationAddress = value;
        }
    }

    private string _destinationCity = string.Empty;
    [Required(ErrorMessage = "Destination city is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Destination city must be between 5 and 20 characters")]
    [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Destination city can only contain letters and spaces")]
    public string DestinationCity
    {
        get
        {
            return _destinationCity;
        }
        init
        {
            _destinationCity = value;
        }
    }

    private CargoType _cargoType = CargoType.General;
    [Required(ErrorMessage = "Cargo type is required")]
    [EnumDataType(typeof(CargoType), ErrorMessage = "Invalid cargo type")]
    [Range(0, int.MaxValue, ErrorMessage = "Invalid cargo type")]
    public CargoType CargoType
    {
        get
        {
            return _cargoType;
        }
        init
        {
            _cargoType = value;
        }
    }
}
