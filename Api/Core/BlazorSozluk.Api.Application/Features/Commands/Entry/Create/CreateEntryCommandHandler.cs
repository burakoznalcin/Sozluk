using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.Create
{
    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IEntryRepository entryRepository;

        public CreateEntryCommandHandler(IMapper mapper, IEntryRepository entryRepository)
        {
            this.mapper = mapper;
            this.entryRepository = entryRepository; 
        }

        public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var dbEntry = mapper.Map<BlazorSozlukApi.Domain.Models.Entry>(request);

            await entryRepository.AddAsync(dbEntry);   
            
            return dbEntry.Id;
           
        }
    }
}
