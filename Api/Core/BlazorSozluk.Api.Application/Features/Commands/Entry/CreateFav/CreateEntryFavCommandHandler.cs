using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.Entry;
using BlazorSozlukCommon.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {

        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.FavExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.CreateEntryFavQueueName, new EntryFavCommandEvent()
            {
                CreatedById = request.UserId,
                EntryCommentId = request.EntryId
            });

            return Task.FromResult(true);
        }
    }
}
