namespace BashSoft.Models
{
    using System;
    using System.Collections.Generic;
    using BashSoft.Contracts;
    using Exceptions;

    public class SoftUniCourse : ICourse
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        private string name;

        private Dictionary<string, IStudent> studentsByName;

        public SoftUniCourse(string name)
        {
            this.name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get => this.name;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName => this.studentsByName;

        public void EnrollStudent(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, this.name);
            }

            this.studentsByName.Add(student.Username, student);
        }

        public int CompareTo(ICourse other)
        {
            return string.Compare(this.Name, other.Name, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}