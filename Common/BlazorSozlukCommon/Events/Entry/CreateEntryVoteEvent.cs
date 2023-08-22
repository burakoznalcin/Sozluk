using BlazorSozlukCommon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukCommon.Events.Entry
{
    public class CreateEntryVoteEvent
    {
        public Guid EntryId { get; set; }
        public VoteType voteType{ get; set; }

        public Guid CreatedBy { get; set; }


    }
}
