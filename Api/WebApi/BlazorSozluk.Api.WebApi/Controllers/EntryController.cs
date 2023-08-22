using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetEntries.GetMainPageEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;
using BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;
using BlazorSozlukCommon.ViewModels.Queries;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator meditor;
        public EntryController(IMediator meditor)
        {
            this.meditor = meditor;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var entries = await meditor.Send(query);
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await meditor.Send(new GetEntryDetailQuery(id,UserID));

            return Ok(result);
        }

        [HttpGet("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await meditor.Send(new GetEntryCommentsQuery(entryId, UserID, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("UserEntries")]
        public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
        {
            if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserID.Value;

            var result = await meditor.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page,int pageSize)
        {
            var entries = await meditor.Send(new GetMainPageEntriesQuery(UserID,page, pageSize));
            return Ok(entries);
        }

        [HttpPost]
        [Route("CreateEntry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
        {
            var result = await meditor.Send(command);

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
        {
            
            var result = await meditor.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var result = await meditor.Send(query);

            return Ok(result);
        }

    }
}
