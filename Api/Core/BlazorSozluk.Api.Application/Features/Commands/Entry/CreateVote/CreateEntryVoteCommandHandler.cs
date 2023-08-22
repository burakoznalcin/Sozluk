using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.Entry;
using BlazorSozlukCommon.Infastructure;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {

        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.VoteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.CreateEntryVoteQueueName, new CreateEntryVoteEvent()
            {
                CreatedBy= request.CreatedBy,
                EntryId = request.EntryId,
                voteType = request.voteType
            });
            return await Task.FromResult(true);
         }
    }
}
