namespace api.Repositories.Contracts;

public interface IDocumentRepository
{
    Task SyncDocumentsFromExternalSource(string email);
}