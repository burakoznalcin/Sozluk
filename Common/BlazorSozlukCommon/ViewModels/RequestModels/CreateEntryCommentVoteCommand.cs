using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukCommon.ViewModels.RequestModels
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
        public VoteType voteType { get; set; }

        public CreateEntryCommentVoteCommand(Guid entryCommentId, Guid userId, VoteType voteType)
        {
            EntryCommentId = entryCommentId;
            UserId = userId;
            this.voteType = voteType;
        }
    }
}
