using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.Entry;
using BlazorSozlukCommon.Events.EntryComment;
using BlazorSozlukCommon.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote
{
    public  class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.DeleteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.DeleteEntryCommentVoteQueueName, new DeleteEntryCommentVoteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedById = request.CreatedById
            }) ;
            return await Task.FromResult(true);
        }
    }
}
