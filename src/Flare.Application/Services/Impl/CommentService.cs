using System.Security.Claims;
using Flare.Application.Models.Comment;
using Flare.DataAccess;
using Flare.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Flare.Application.Services.Impl;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _accessor;

    public CommentService(IUnitOfWork unitOfWork, IHttpContextAccessor accessor)
    {
        _unitOfWork = unitOfWork;
        _accessor = accessor;
    }

    public async Task<CreateCommentResponseModel> CreateCommentAsync(CreateCommentModel createCommentModel)
    {
        string? createdBy = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (createdBy == null) throw new Exception("Author of a comment is undefined");

        var post = await _unitOfWork.Posts.GetAsync(x => x.Id == createCommentModel.PostId);
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Text = createCommentModel.Text,
            CreatedBy = createdBy,
            Post = post,
            CreatedOn = DateTime.UtcNow
        };

        await _unitOfWork.Comments.AddAsync(comment);
        return new CreateCommentResponseModel { Id = comment.Id };
    }

    public async Task<UpdateCommentResponseModel> UpdateCommentAsync(UpdateCommentModel updateCommentModel)
    {
        var comment = await _unitOfWork.Comments.GetAsync(x => x.Id == updateCommentModel.Id);
        var accountName = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (comment == null) throw new Exception("Comment not found");
        if (accountName != comment.CreatedBy) throw new Exception("Incorrect author of the post");

        var updatedComment = new Comment()
        {
            Id = comment.Id,
            Text = updateCommentModel.Text,
            CreatedBy = comment.CreatedBy
        };

        await _unitOfWork.Comments.UpdateAsync(updatedComment);
        return new UpdateCommentResponseModel { Id = updatedComment.Id };
    }

    public async Task<DeleteCommentResponseModel> DeleteCommentAsync(DeleteCommentModel deleteCommentModel)
    {
        var comment = await _unitOfWork.Comments.GetAsync(x => x.Id == deleteCommentModel.Id);
        var accountName = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        if (comment == null) throw new Exception("Comment not found");
        if (accountName != comment.CreatedBy) throw new Exception("Incorrect author of the post");

        await _unitOfWork.Comments.RemoveAsync(comment);
        return new DeleteCommentResponseModel { Id = comment.Id }; 
    }
}