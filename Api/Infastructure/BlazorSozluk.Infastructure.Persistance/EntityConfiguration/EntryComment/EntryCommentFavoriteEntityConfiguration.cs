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
    public class EntryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<BlazorSozlukApi.Domain.Models.EntryCommentFavorite>
    {
        public override void Configure(EntityTypeBuilder<BlazorSozlukApi.Domain.Models.EntryCommentFavorite> builder)
        {
            base.Configure(builder);
            builder.ToTable("entrycommentfavorite", BlazorSozlukContext.DEFAULT_SCHEMA);

            builder.HasOne(i => i.EntryComment)
                .WithMany(i => i.EntryCommentFavorite)
                .HasForeignKey(i => i.EntryCommentId);

            builder.HasOne(i => i.CreatedUser)
                .WithMany(i => i.EntryCommentFavorite)
                .HasForeignKey(i => i.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
