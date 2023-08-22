using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukCommon.Events.User
{
    public class UserEmailChangedEvent
    {
        public string OldEmailAddress { get; set; }
        public string NewEmailAddress { get; set; } 
    }
}
