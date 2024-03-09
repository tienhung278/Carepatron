using api.Repositories.Contracts;
using api.Services.Contracts;
using AutoMapper;

namespace api.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IClientService> _lazyClientServices;

    public IClientService ClientService => _lazyClientServices.Value;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _lazyClientServices = new Lazy<IClientService>(() => new ClientService(repositoryManager, mapper));
    }
}