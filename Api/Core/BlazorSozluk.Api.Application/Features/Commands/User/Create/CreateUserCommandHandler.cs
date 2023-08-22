using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.User;
using BlazorSozlukCommon.Infastructure;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler :IRequestHandler<CreateUserCommand,Guid>
    {    
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (existsUser is not null)
                throw new DatabaseValidateExceptions("User already exists!");

            var dbUser = mapper.Map<BlazorSozlukApi.Domain.Models.User>(request);

            var rows = await userRepository.AddAsync(dbUser);

            if (rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };

                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserEmailExchangeName,
                                                   exchangeType: SozlukConstants.DefaultExchangeType,
                                                   queueName: SozlukConstants.UserEmailChangedQueueName,
                                                   obj: @event);
            }

            return dbUser.Id;

        }

    }
}
