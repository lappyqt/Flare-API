namespace Flare.DataAccess.Repositories.Impl;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(DatabaseContext context): base(context) {}
}