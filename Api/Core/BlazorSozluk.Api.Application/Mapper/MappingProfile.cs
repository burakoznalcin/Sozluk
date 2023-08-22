using AutoMapper;
using BlazorSozlukApi.Domain.Models;
using BlazorSozlukCommon.ViewModels.Queries;
using BlazorSozlukCommon.ViewModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

            CreateMap<CreateUserCommand, User>();

            CreateMap<UpdateUserCommand, User>();

            CreateMap<CreateEntryCommand, User>()
                .ReverseMap();

            CreateMap<CreateEntryCommentCommand, EntryComment>()
                .ReverseMap();

            CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x=>x.CommentCount, y=> y.MapFrom(z=> z.EntryComments.Count)); 
            
        

        }
    }
}
