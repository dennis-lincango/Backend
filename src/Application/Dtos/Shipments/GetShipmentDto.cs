using Domain.Enums;

namespace Application.Dtos.Shipments;

public record GetShipmentDto
{
    private uint _id = uint.MinValue;
    public uint Id
    {
        get
        {
            return _id;
        }
        init
        {
            _id = value;
        }
    }

    private string _description = string.Empty;
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
