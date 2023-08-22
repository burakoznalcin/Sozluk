using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.Infastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailConfirmationRepository emailConfirmationRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailConfirmationRepository emailConfirmationRepository)
        {
            this.userRepository = userRepository;
            this.emailConfirmationRepository = emailConfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await emailConfirmationRepository.GetByIdAsync(request.ConfirmationId);

            if (confirmation is null)
                throw new DatabaseValidateExceptions("Confirmation not found!");

            var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == confirmation.NewEmailAddress);

            if (dbUser is null)
                throw new DatabaseValidateExceptions("User not found with this email!");

            if (dbUser.EmailConfirmed)
                throw new DatabaseValidateExceptions("Email address is already confirmed");

            dbUser.EmailConfirmed = true;
            await userRepository.UpdateAsync(dbUser);

            return true;




        }
    }
}
