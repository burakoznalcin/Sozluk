using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukApi.Domain.Models
{
    public class User :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool  EmailConfirmed { get; set; }

        public virtual ICollection<Entry> Entries{ get; set; }
        public virtual ICollection<EntryFavorite> EntryFavorite { get; set; } 

        public virtual ICollection<EntryComment> EntryComment{ get; set; }
        public virtual ICollection<EntryCommentFavorite> EntryCommentFavorite { get; set; }

    }
}
