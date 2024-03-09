namespace api.Repositories.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}