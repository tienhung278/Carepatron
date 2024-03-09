namespace api.Repositories.Contracts;

public interface IRepositoryManager
{
    public IClientRepository ClientRepository { get; }
    public IDocumentRepository DocumentRepository { get; }
    public IEmailRepository EmailRepository { get; }
    public IUnitOfWork UnitOfWork { get; }
}