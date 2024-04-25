namespace Lawyeed.API.Lawyeed.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}