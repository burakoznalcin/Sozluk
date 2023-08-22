using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.ViewModels.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public GetUserDetailQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            BlazorSozlukApi.Domain.Models.User dbUser = null;

            if (request.UserId != Guid.Empty)
                dbUser = await userRepository.GetByIdAsync(request.UserId);
            else if (!string.IsNullOrEmpty(request.UserName))
                dbUser = await userRepository.GetSingleAsync(i => i.UserName == request.UserName);
            else
                throw new DatabaseValidateExceptions("User not found!");

            return mapper.Map<UserDetailViewModel>(dbUser); 




        }
    }
}
