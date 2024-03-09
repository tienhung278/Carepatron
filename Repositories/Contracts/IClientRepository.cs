using api.Models;

namespace api.Repositories.Contracts;

public interface IClientRepository
{
    Task<Client[]> Get();
    Task Create(Client client);
    Task Update(Client client);
}