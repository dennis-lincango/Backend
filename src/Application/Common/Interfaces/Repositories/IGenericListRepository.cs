namespace Application.Common.Interfaces.Repositories;

public interface IGenericListRepository<T>
{
    public void Add(string key, T item, TimeSpan? expiresAtMinutes = null);
    public bool Exists(string key);
    public void Remove(string key);
}
