using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukCommon.ViewModels.RequestModels
{
    public class CreateEntryVoteCommand: IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid CreatedBy { get; set; }
        public VoteType voteType { get; set; }

        public CreateEntryVoteCommand(Guid entryId, Guid createdBy, VoteType voteType)
        {
            EntryId = entryId;
            CreatedBy = createdBy;
            this.voteType = voteType;
        }
    }
}
