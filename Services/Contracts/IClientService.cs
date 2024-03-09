using api.Dtos;

namespace api.Services.Contracts;

public interface IClientService
{
    Task<IReadOnlyList<ClientReadDto>> FindClientsAsync();
    Task<ClientReadDto> FindClientAsync(Guid id);
    Task<Guid> CreateClientAsync(ClientWriteDto serviceWriteDto);
    Task UpdateClientAsync(Guid id, ClientWriteDto serviceWriteDto);
}