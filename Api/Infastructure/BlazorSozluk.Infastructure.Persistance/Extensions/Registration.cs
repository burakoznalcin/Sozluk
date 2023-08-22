using BlazorSozluk.Infastructure.Persistance.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Infastructure.Persistance.Repositories;

namespace BlazorSozluk.Infastructure.Persistance.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {
                var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
                conf.UseSqlServer(connStr, opt=>
                {
                    opt.EnableRetryOnFailure();
                });
            });
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryCommentFavoriteRepository, EntryCommentFavoriteRepository>();
            services.AddScoped<IEntryCommentVoteRepository, EntryCommentVoteRepository>();
            services.AddScoped<IEntryFavoriteRepository, EntryFavoriteRepository>();
            services.AddScoped<IEntryVoteRepository, EntryVoteRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            return services;
        }
    }
}
