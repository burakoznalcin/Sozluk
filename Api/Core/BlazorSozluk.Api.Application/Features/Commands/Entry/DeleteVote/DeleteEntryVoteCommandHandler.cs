using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.Entry;
using BlazorSozlukCommon.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.DeleteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryVoteQueueName, new DeleteEntryVoteEvent()
            {
                CreatedBy = request.UserId,
                EntryId = request.EntryId
            });
            return await Task.FromResult(true);
        }
    }
}
