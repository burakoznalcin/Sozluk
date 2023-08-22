using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.EntryComment;
using BlazorSozlukCommon.Infastructure;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(SozlukConstants.VoteExchangeName, SozlukConstants.DefaultExchangeType, SozlukConstants.CreateEntryCommentVoteQueueName, new CreateEntryCommentVoteEvent()
            {
                CreatedById = request.UserId,
                EntryCommentId = request.EntryCommentId,
                voteType = request.voteType
            });

            return await Task.FromResult(true);
        }
    }
}
