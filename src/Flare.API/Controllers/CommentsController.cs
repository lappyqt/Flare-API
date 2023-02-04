using Flare.Application.Models.Comment;
using Flare.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flare.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentsController(ICommentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCommentAsync(CreateCommentModel createCommentModel)
    {
        return Ok(await _service.CreateCommentAsync(createCommentModel));
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateCommentAsync(UpdateCommentModel updateCommentModel)
    {
        return Ok(await _service.UpdateCommentAsync(updateCommentModel));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCommentAsync(DeleteCommentModel deleteCommentModel)
    {
        return Ok(await _service.DeleteCommentAsync(deleteCommentModel));
    }
}