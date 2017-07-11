﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BashSoft.Exceptions;

namespace BashSoft.Models
{
    public class Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;

        public Student(string userName)
        {
            this.UserName = userName;
            this.enrolledCourses = new Dictionary<string, Course>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string UserName
        {
            get { return this.userName; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.userName = value;
            }
        }

        public IReadOnlyDictionary<string, Course> EnrolledCourses
        {
            get { return enrolledCourses; }
            
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get { return marksByCourseName; }
        }

        public void EnrollInCourse(Course course)
        {
            if (this.EnrolledCourses.ContainsKey(course.Name))
            {
                throw new DublicateEntryInStructureException( this.UserName, course.Name);
               
            }

            this.enrolledCourses.Add(course.Name, course);
        }

        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.EnrolledCourses.ContainsKey(courseName))
            {
                throw new KeyNotFoundException(ExceptionMessages.NotEnrolledInCourse);
              
            }

            if (scores.Length > Course.NumberOfTasksOnExam)
            {
               throw new ArgumentException(ExceptionMessages.InvalidNumberOfScores);
               
            }

            this.marksByCourseName.Add(courseName,CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double persentageOfSolvedExam = scores.Sum() /
                                            (double) (Course.NumberOfTasksOnExam * Course.MaxScoreOnExamTask);
            double mark = persentageOfSolvedExam * 4 + 2;
            return mark;
        }
    }
}