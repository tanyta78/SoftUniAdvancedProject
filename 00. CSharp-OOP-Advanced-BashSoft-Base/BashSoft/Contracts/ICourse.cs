﻿namespace BashSoft.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ICourse : IComparable<ICourse>
    {
        string Name { get; set; }

        IReadOnlyDictionary<string, IStudent> StudentsByName { get; }

        void EnrollStudent(IStudent student);
    }
}