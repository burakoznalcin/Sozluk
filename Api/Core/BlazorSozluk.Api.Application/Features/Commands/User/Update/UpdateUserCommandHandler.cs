using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon;
using BlazorSozlukCommon.Events.User;
using BlazorSozlukCommon.Infastructure;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);


            if (dbUser is null)
                throw new DatabaseValidateExceptions("User not exists!");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            mapper.Map(request, dbUser);

            var rows = await userRepository.UpdateAsync(dbUser);

            if (rows > 0 && emailChanged)
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
                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;
        }
    }
}
