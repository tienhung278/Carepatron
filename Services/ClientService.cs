using api.Dtos;
using api.Models;
using api.Repositories.Contracts;
using api.Services.Contracts;
using AutoMapper;

namespace api.Services;

public class ClientService : IClientService
{
    private readonly IMapper _mapper;
    private readonly IClientRepository _clientRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly IEmailRepository _emailRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _mapper = mapper;
        _clientRepository = repositoryManager.ClientRepository;
        _documentRepository = repositoryManager.DocumentRepository;
        _emailRepository = repositoryManager.EmailRepository;
        _unitOfWork = repositoryManager.UnitOfWork;
    }

    public async Task<IReadOnlyList<ClientReadDto>> FindClientsAsync()
    {
        var clients = await _clientRepository.FindAllAsync();
        return _mapper.Map<IReadOnlyList<ClientReadDto>>(clients);
    }
    
    public async Task<IReadOnlyList<ClientReadDto>> FindClientByNameAsync(string name)
    {
        var clients = await _clientRepository.FindAsync(c =>
            c.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
            c.LastName.Contains(name, StringComparison.OrdinalIgnoreCase));
        return _mapper.Map<IReadOnlyList<ClientReadDto>>(clients);
    }

    public async Task<ClientReadDto> FindClientAsync(Guid id)
    {
        var client = await _clientRepository.FindByIdAsync(id);
        return _mapper.Map<ClientReadDto>(client);
    }

    public async Task<Guid> CreateClientAsync(ClientWriteDto clientWriteDto)
    {
        var client = _mapper.Map<Client>(clientWriteDto);
        client.Id = new Guid();
        await _clientRepository.CreateAsync(client);
        /*await _emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
        await _documentRepository.SyncDocumentsFromExternalSource(client.Email);*/
        await _unitOfWork.SaveChangesAsync();
        return client.Id;
    }

    public async Task UpdateClientAsync(Guid id, ClientWriteDto clientWriteDto)
    {
        var client = await _clientRepository.FindByIdAsync(id);
        if (client == null)
            return;

        if (client.Email != clientWriteDto.Email)
        {
            /*await _emailRepository.Send(client.Email, "Hi there - welcome to my Carepatron portal.");
            await _documentRepository.SyncDocumentsFromExternalSource(client.Email);*/
        }
        client.FirstName = clientWriteDto.FirstName;
        client.LastName = clientWriteDto.LastName;
        client.Email = clientWriteDto.Email;
        client.PhoneNumber = clientWriteDto.PhoneNumber;
        await _clientRepository.UpdateAsync(client);
        await _unitOfWork.SaveChangesAsync();
    }
}