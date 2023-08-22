using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommand : IRequest<bool>
    {
        public Guid EntryId { get; set; }
        public Guid CreatedBy { get; set; }

        public DeleteEntryFavCommand(Guid entryId, Guid createdBy)
        {
            EntryId = entryId;
            CreatedBy = createdBy;
        }
    }
}
