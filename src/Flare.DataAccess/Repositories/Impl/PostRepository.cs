namespace Flare.DataAccess.Repositories.Impl;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(DatabaseContext context): base(context) {}
}