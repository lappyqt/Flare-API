namespace Flare.DataAccess.Repositories.Impl;

public class AccountRepository : BaseRepository<Account>, IAccountRepository
{
    public AccountRepository(DatabaseContext context): base(context) {}
}