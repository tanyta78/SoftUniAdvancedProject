using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
   public class Course
   {
       private string name;
       private Dictionary<string, Student> studentsByName;

       public const int NumberOfTasksOnExam = 5;
       public const int MaxScoreOnExamTask = 100;

        public Course(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, Student>();
        }

       public string Name
       {
           get { return this.name; }
           
           private set
           {
               if (string.IsNullOrEmpty(value))
               {
                   throw new InvalidStringException();
               }

               this.name = value;
           }
       }

       public IReadOnlyDictionary<string, Student> StudentsByName
       {
           get { return studentsByName; }
         
       }

       public void EnrollStudent(Student student)
       {
           if (this.StudentsByName.ContainsKey(student.UserName))
           {
               throw new DublicateEntryInStructureException(student.UserName,this.Name);
               
           }

           this.studentsByName.Add(student.UserName,student);
       }
    }
}
