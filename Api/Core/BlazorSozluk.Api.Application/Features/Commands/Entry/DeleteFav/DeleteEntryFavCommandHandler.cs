using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.Entry;
using BlazorSozlukCommon.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.DeleteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryFavQueueName, new DeleteEntryVoteEvent()
            {
                CreatedBy = request.CreatedBy,
                EntryId = request.EntryId
            });
            return Task.FromResult(true);
        }
    }
}
