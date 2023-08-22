using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.EntryComment;
using BlazorSozlukCommon.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {
        public Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.DeleteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryCommentFavQueueName, new DeleteEntryCommentFavEvent()
            {
                CreatedById = request.UserId,
                EntryCommentId = request.EntryCommentId
            });
            return Task.FromResult(true);
        }
    }
}
