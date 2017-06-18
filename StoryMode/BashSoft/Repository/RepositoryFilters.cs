namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter, int studentsToTake)
        {
            wantedFilter = wantedFilter.ToLower();
            Predicate<double> filterToUse = null;
            if (wantedFilter == "excellent")
            {
                filterToUse = x => x >= 5;
            }
            else if (wantedFilter == "average")
            {
                filterToUse = x => x < 5 && x >= 3.5;
            }
            else if (wantedFilter == "poor")
            {
                filterToUse = x => x < 3.5;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);

                return;
            }

            FilterAndTake(wantedData, filterToUse, studentsToTake);
        }

        private static void FilterAndTake(Dictionary<string, List<int>> wantedData, Predicate<double> givenFilter, int studentsToTake)
        {
            int countForPrinted = 0;
            foreach (var userName_Points in wantedData)
            {
                if (countForPrinted == studentsToTake)
                {
                    break;
                }

                double averageScore = userName_Points.Value.Average();
                double percentageOfFullfilment = averageScore / 100;
                double mark = percentageOfFullfilment * 4 + 2;
                if (givenFilter(mark))
                {
                    OutputWriter.PrintStudent(userName_Points);
                    countForPrinted++;
                }
            }
        }

        private static double Average(List<int> scoresOnTask)
        {
            double totalScore = 0.0;
            foreach (int score in scoresOnTask)
            {
                totalScore += score;
            }

            double percentageOfAll = totalScore / (scoresOnTask.Count * 100.0);
            double mark = percentageOfAll * 4 + 2;

            return mark;
        }
    }
}
