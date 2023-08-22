using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.EntryComment;
using BlazorSozlukCommon.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateFav
{
    public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.FavExchangeName,SozlukConstants.DefaultExchangeType,SozlukConstants.CreateEntryCommentFavQueueName,new CreateEntryCommentFavEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedById = request.UserId
            });

            return await Task.FromResult(true);
        }
    }
}
