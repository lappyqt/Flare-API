namespace Flare.DataAccess.Repositories.Impl;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(DatabaseContext context): base(context) {}
}