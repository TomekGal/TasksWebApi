using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksWebApi.Exceptions
{
    public class MailTakenException: Exception
    {
        public MailTakenException(string message) : base(message)
        {

        }
    }
}
