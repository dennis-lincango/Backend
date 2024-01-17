namespace Domain.Common;

public class BaseEntity<TKey>
{
    private TKey _id = default!;
    public TKey Id
    {
        get
        {
            return _id;
        }
        private set
        {
            _id = value;
        }
    }
}