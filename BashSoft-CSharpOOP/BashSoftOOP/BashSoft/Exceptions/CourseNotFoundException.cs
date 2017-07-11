﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public class CourseNotFoundException:Exception
    {
        private const string InexistingCourseInDataBase =
            "The course you are trying to get does not exist in the data base!";

        public CourseNotFoundException():base(InexistingCourseInDataBase)
        {
        }

        public CourseNotFoundException(string message) : base(message)
        {
        }
    }
}
