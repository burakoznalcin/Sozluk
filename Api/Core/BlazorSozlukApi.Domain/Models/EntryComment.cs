﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukApi.Domain.Models
{
    public class EntryComment : BaseEntity
    {
        public string Content { get; set; }
        public Guid CreatedById { get; set; }

        public Guid EntryId { get; set; }
        public virtual Entry Entry { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual ICollection<EntryCommentVote> EntryCommentVote { get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorite { get; set; }


    }
}
