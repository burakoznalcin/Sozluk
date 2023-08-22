using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.Infastructure;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.ViewModels.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;
        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser == null)
                throw new DatabaseValidateExceptions("User not FOUND!");

            var encPass = PasswordEncryptor.Encrpt(request.OldPassword);

            if (dbUser.Password != encPass)
                throw new DatabaseValidateExceptions("Old password wrong!");

            var newPassEncred = PasswordEncryptor.Encrpt(request.NewPassword);

            dbUser.Password = newPassEncred;

            await userRepository.UpdateAsync(dbUser);

            return true;
        }
    }
}
