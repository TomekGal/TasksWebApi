﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksWebApi.Exceptions
{
    public class ForbidException: Exception 
    {
        public ForbidException(string message):base(message)
        {

        }
    }
}
