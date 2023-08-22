using BlazorSozluk.Infastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.EntityConfiguration.EntryComment
{
    public class EntryCommentEntityConfiguration : BaseEntityConfiguration<BlazorSozlukApi.Domain.Models.EntryComment>
    {
        public override void Configure(EntityTypeBuilder<BlazorSozlukApi.Domain.Models.EntryComment> builder)
        {
            base.Configure(builder);
            builder.ToTable("entrycomment", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.CreatedBy)
                .WithMany(i => i.EntryComment)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Entry)
                .WithMany(i => i.EntryComments)
                .HasForeignKey(i => i.EntryId);
        }

    }
}
