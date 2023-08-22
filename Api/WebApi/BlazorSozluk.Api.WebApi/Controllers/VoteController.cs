using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozlukCommon.ViewModels;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator mediator;

        public VoteController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId,VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryVoteCommand(entryId, UserID.Value,  voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, UserID.Value, voteType));

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            await mediator.Send(new DeleteEntryVoteCommand(entryId, UserID.Value));

            return Ok();
        }
        [HttpPost]
        [Route("DeleteEntryComment/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            await mediator.Send(new DeleteEntryVoteCommand(entryCommentId, UserID.Value));

            return Ok();
        }


    }
}
