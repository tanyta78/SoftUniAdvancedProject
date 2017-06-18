using System;
using System.Collections.Generic;
using System.Linq;

namespace BashSoft
{
    public static class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter,
            int studentsToTake)
        {
            switch (wantedFilter)
            {
                case "excellent":
                    FilterAndTake(wantedData, x => x >= 5, studentsToTake);
                    break;

                case "average":
                    FilterAndTake(wantedData, x => x < 5 && x >= 3.5, studentsToTake);
                    break;

                case "poor":
                    FilterAndTake(wantedData, x => x < 3.5, studentsToTake);
                    break;

                default:
                    OutputWriter.DisplayException(ExceptionMessages.InvalidStudentsFilter);
                    break;
            }
        }

        private static void FilterAndTake(Dictionary<string, List<int>> wantedData,
            Predicate<double> givenFilter,
            int studentsToTake)
        {
            var counterForPrintedStudents = 0;

            foreach (var pair in wantedData)
            {
                if (counterForPrintedStudents == studentsToTake)
                {
                    break;
                }

                double averageScore = pair.Value.Average();
                double persentageOfFullfilment = averageScore / 100;
                double mark = persentageOfFullfilment * 4 + 2;

                if (givenFilter(mark))
                {
                    OutputWriter.PrintStudent(pair);
                    counterForPrintedStudents++;
                }
            }
        }

        /*
                private static bool ExcellentFilter(double mark)
                {
                    return mark >= 5.0;
                }

                private static bool AverageFilter(double mark)
                {
                    return mark < 5.0 && mark >= 3.5;
                }

                private static bool PoorFilter(double mark)
                {
                    return mark < 3.5;
                }

                private static double Average(List<int> scoresOnTasks)
                {
                    int totalScore = 0;
                    foreach (var score in scoresOnTasks)
                    {
                        totalScore += score;
                    }

                    var percentageOfAll = totalScore / scoresOnTasks.Count * 100;
                    var mark = percentageOfAll * 4 + 2;

                    return mark;
                }
                */
    }
}