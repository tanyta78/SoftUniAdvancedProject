﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public class InvalidPathException:Exception
    {
        private static string InvalidPath =
            "The folder/file you are trying to access at the current address, does not exist.";

        public InvalidPathException() : base(InvalidPath)
        {
            
        }

        public InvalidPathException(string message) : base(message)
        {
            
        }
       
    }
}
