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
    public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
    {
        public EmailConfirmationRepository(BlazorSozlukContext dbcontext) : base(dbcontext)
        {
        }
    }
}
