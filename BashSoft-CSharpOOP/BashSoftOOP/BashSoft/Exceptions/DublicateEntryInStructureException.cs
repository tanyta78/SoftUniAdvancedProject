using System;

namespace BashSoft.Exceptions
{
    public class DublicateEntryInStructureException : Exception
    {
        private const string DuplicateEntry = "The {0} already exists in {1}.";

        public DublicateEntryInStructureException(string message) : base(message)
        {
        }

        public DublicateEntryInStructureException(string entry, string structure) : base(String.Format(DuplicateEntry, entry, structure))
        {
        }
    }
}