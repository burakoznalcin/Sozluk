using BlazorSozluk.Infastructure.Persistance.Context;
using BlazorSozlukApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.EntityConfiguration
{
    public class UserEntityConfiguration:BaseEntityConfiguration<User>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("user", BlazorSozlukContext.DEFAULT_SCHEMA);
        }
    }
}
