using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public class DeleteEntryCommentVoteCommand: IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid CreatedById { get; set; }
        public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid createdById)
        {
            EntryCommentId = entryCommentId;
            CreatedById = createdById;
        }
    }
}
