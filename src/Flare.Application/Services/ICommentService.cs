using Flare.Application.Models.Comment;

namespace Flare.Application.Services;

public interface ICommentService
{
    Task<CreateCommentResponseModel> CreateCommentAsync(CreateCommentModel createCommentModel);
    Task<UpdateCommentResponseModel> UpdateCommentAsync(UpdateCommentModel updateCommentModel);
    Task<DeleteCommentResponseModel> DeleteCommentAsync(DeleteCommentModel deleteCommentModel);
}