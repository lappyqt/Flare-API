using Flare.DataAccess.Repositories;
using Flare.DataAccess.Repositories.Impl;

namespace Flare.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public IPostRepository Posts { get; private set; }
    public IAccountRepository Accounts { get; private set;}
    public ICategoryRepository Categories { get; private set; }
    public ICommentRepository Comments { get; private set; }

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;

        Posts = new PostRepository(context);
        Accounts = new AccountRepository(context);
        Categories = new CategoryRepository(context);
        Comments = new CommentRepository(context);
    }

    public async Task CompleteAsync() => await _context.SaveChangesAsync();
    public void Dispose() => _context.Dispose();
}