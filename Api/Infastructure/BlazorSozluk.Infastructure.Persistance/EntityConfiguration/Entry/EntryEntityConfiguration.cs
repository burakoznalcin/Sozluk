using BlazorSozluk.Infastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlazorSozluk.Infastructure.Persistance.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.EntityConfiguration.Entry
{
    public class EntryEntityConfiguration : BaseEntityConfiguration<BlazorSozlukApi.Domain.Models.Entry>
    {
        public virtual void Configure(EntityTypeBuilder<BlazorSozlukApi.Domain.Models.Entry> builder)
        {
           base.Configure(builder);

            builder.ToTable("entry", BlazorSozlukContext.DEFAULT_SCHEMA);
            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.Entries)
                .HasForeignKey(i => i.CreatedById);
        }
    }
}
