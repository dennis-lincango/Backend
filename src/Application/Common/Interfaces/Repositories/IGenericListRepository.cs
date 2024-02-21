namespace Application.Common.Interfaces.Repositories;

/// <summary>
/// Interface for a generic list repository.
/// </summary>
/// <typeparam name="T">The type of items in the list.</typeparam>
public interface IGenericListRepository<T>
{
    /// <summary>
    /// Adds an item to the list.
    /// </summary>
    /// <param name="key">The key to add the item with.</param>
    /// <param name="item">The item to add to the list.</param>
    /// <param name="expiresAtMinutes">Optional expiration time in minutes.</param>
    public void Add(string key, T item, TimeSpan? expiresAtMinutes = null);

    /// <summary>
    /// Checks if an item with the given key exists in the list.
    /// </summary>
    /// <param name="key">The key to check for existence.</param>
    /// <returns>True if the item exists, false otherwise.</returns>
    public bool Exists(string key);

    /// <summary>
    /// Removes an item from the list by its key.
    /// </summary>
    /// <param name="key">The key of the item to remove.</param>
    public void Remove(string key);
}
