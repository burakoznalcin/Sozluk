using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BlazorSozlukCommon.Infastructure.Exceptions
{
    public class DatabaseValidateExceptions : Exception
    {
        public DatabaseValidateExceptions()
        {
        }

        public DatabaseValidateExceptions(string? message) : base(message)
        {
        }

        public DatabaseValidateExceptions(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DatabaseValidateExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
