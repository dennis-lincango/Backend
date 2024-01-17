using Domain.Entities;
using Application.Common.Interfaces.Repositories;

namespace Application.Interfaces.Repositories;

public interface ICustomersRepositoryAsync : IGenericRepositoryAsync<Customer, uint>
{

}
