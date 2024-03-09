namespace api.Repositories.Contracts;

public interface IEmailRepository
{
    Task Send(string email, string message);
}