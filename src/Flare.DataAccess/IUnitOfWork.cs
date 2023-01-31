using Flare.DataAccess.Repositories;

namespace Flare.DataAccess;

public interface IUnitOfWork : IDisposable
{
    IPostRepository Posts { get; }
    IAccountRepository Accounts { get; }
    ICategoryRepository Categories { get; }
    ICommentRepository Comments { get; }
    Task CompleteAsync();
}