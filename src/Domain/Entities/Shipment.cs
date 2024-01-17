using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Shipment : BaseEntity<uint>
{
    private string _description = string.Empty;
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = value;
        }
    }

    private double _weight = +0.0;
    public double Weight
    {
        get
        {
            return _weight;
        }
        set
        {
            _weight = value;
        }
    }

    private double _length = +0.0;
    public double Length
    {
        get
        {
            return _length;
        }
        set
        {
            _length = value;
        }
    }

    private double _width = +0.0;
    public double Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value;
        }
    }

    private double _height = +0.0;
    public double Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
        }
    }

    private string _sourceAddress = string.Empty;
    public string SourceAddress
    {
        get
        {
            return _sourceAddress;
        }
        set
        {
            _sourceAddress = value;
        }
    }

    private string _sourceCity = string.Empty;
    public string SourceCity
    {
        get
        {
            return _sourceCity;
        }
        set
        {
            _sourceCity = value;
        }
    }

    private string _destinationAddress = string.Empty;
    public string DestinationAddress
    {
        get
        {
            return _destinationAddress;
        }
        set
        {
            _destinationAddress = value;
        }
    }

    private string _destinationCity = string.Empty;
    public string DestinationCity
    {
        get
        {
            return _destinationCity;
        }
        set
        {
            _destinationCity = value;
        }
    }

    private CargoType _cargoType = CargoType.General;
    public CargoType CargoType
    {
        get
        {
            return _cargoType;
        }
        set
        {
            _cargoType = value;
        }
    }

    private uint _customerId = uint.MinValue;
    public uint CustomerId
    {
        get
        {
            return _customerId;
        }
        set
        {
            _customerId = value;
        }
    }

    private Customer _customer = null!;
    public Customer Customer
    {
        get
        {
            return _customer;
        }
        set
        {
            _customer = value;
        }
    }
}
