namespace Application.Common.Exceptions;

public class NotFoundException : Exception
{
    private string _name = string.Empty;
    public string Name
    {
        get
        {
            return _name;
        }
        init
        {
            _name = value;
        }
    }

    private object _key = null!;
    public object Key
    {
        get
        {
            return _key;
        }
        init
        {
            _key = value;
        }
    }

    public NotFoundException()
        : base()
    {
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
        Name = name;
        Key = key;
    }
}
