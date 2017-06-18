using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public static class RepositorySorters
    {
        public static void OrderAndTake(Dictionary<string, List<int>> wantedData, string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            switch (comparison)
            {
                case "ascending":
                    PrintStudents(wantedData.OrderBy(x => x.Value.Sum())
                        .Take(studentsToTake)
                        .ToDictionary(pair => pair.Key, pair => pair.Value));
                    break;

                case "descending":
                    PrintStudents(wantedData.OrderByDescending(x => x.Value.Sum())
                             .Take(studentsToTake)
                             .ToDictionary(pair => pair.Key, pair => pair.Value));
                    break;

                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
                    break;
            }
        }

        private static void PrintStudents(Dictionary<string, List<int>> studentsSorted)
        {
            foreach (var pair in studentsSorted)
            {
                OutputWriter.PrintStudent(pair);
            }
        }
    }
}