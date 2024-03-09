using api.Data;
using api.Repositories.Contracts;

namespace api.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IClientRepository> _lazyClientRepository;
    private readonly Lazy<IDocumentRepository> _lazyDocumentRepository;
    private readonly Lazy<IEmailRepository> _lazyEmailRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

    public IClientRepository ClientRepository => _lazyClientRepository.Value;
    public IDocumentRepository DocumentRepository => _lazyDocumentRepository.Value;
    public IEmailRepository EmailRepository => _lazyEmailRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;

    public RepositoryManager(DataContext dataContext)
    {
        _lazyClientRepository = new Lazy<IClientRepository>(() => new ClientRepository(dataContext));
        _lazyDocumentRepository = new Lazy<IDocumentRepository>(() => new DocumentRepository(dataContext));
        _lazyEmailRepository = new Lazy<IEmailRepository>(() => new EmailRepository(dataContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dataContext));
    }
}