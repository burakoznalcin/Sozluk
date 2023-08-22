using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid EntryCommentId { get; set; }
        public DeleteEntryCommentFavCommand(Guid userId, Guid entryCommentId)
        {
            UserId = userId;
            EntryCommentId = entryCommentId;
        }
    }
}
