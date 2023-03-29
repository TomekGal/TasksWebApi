using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksWebApi.Exceptions
{
    public class ConfirmPasswordException:Exception
    {
        public ConfirmPasswordException(string message):base(message)
        {

        }
    }
}
