﻿using BashSoft.Exceptions;
using BashSoft.IO.Commands;

namespace BashSoft
{
    public class ShowCourseCommand : Command
    {
        public ShowCourseCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 2)
            {
                var course = this.Data[1];
                this.Repository.GetAllStudentsFromCourse(course);
            }
            else if (this.Data.Length == 3)
            {
                var course = this.Data[1];
                var username = this.Data[2];
                this.Repository.GetStudentScoresFromCourse(course, username);
            }
            else
            {
               throw new InvalidCommandException(this.Input);
            }
        }
    }
}