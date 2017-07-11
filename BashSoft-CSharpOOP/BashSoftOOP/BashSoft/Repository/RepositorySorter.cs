﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.Exceptions;

namespace BashSoft
{
   public class RepositorySorter
    {
        public void OrderAndTake(Dictionary<string, double> studentsWithMarks, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {
                this.PrintStudents(studentsWithMarks
                    .OrderBy(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(x => x.Key, x => x.Value));
            }
            else if(comparison == "descending")
            {
               this.PrintStudents(studentsWithMarks
                    .OrderByDescending(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(x => x.Key, x => x.Value));
            }
            else
            {
                throw new InvalidQueryComparisonException();
            }
        }

        public void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (var kv in studentsSorted)
            {
                OutputWriter.PrintStudent(kv);
            }
        }
    }
}
