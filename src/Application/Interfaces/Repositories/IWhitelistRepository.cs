using Application.Common.Interfaces.Repositories;

namespace Application.Interfaces.Repositories
{
    // This code block defines the interface for the Whitelist repository, extending the IGenericListRepository with boolean type
    public interface IWhitelistRepository : IGenericListRepository<bool>
    {
    }
}