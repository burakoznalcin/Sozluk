using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Infastructure.Persistance.Context;
using BlazorSozlukApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BlazorSozlukContext dbcontext) : base(dbcontext)
        {
        }
    }
}
