namespace BashSoft
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;

    public static class StudentRepository
    {
        public static bool IsDataInitialized = false;
        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse;

        public static void InitializeData(string fileName)
        {
            if (!IsDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Reading data...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataAlreadyInitializedException);
            }
        }

        private static void ReadData(string fileName)
        {
            string path = SessionData.CurrentPath + "\\" + fileName;
            string[] allInputLines = null;
            try
            {
                allInputLines = File.ReadAllLines(path);
            }
            catch (FileNotFoundException)
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                return;
            }

            //I HAVE CHANGED THE PATTERN TO MATCH YEARS FROM 2014 to 2017 AND SCORES FROM 0 to 100!!!
            string pattern = @"^([A-Z][a-zA-Z+#]*_[A-Z][a-z]{2}_201[4-7])\s+([A-Z][a-z]{0,3}\d{2}_\d{2,4})\s+(100|[1-9][0-9]|[0-9])$";
            Regex validStudent = new Regex(pattern);

            for (int currentElement = 0; currentElement < allInputLines.Length; currentElement++)
            {
                string currentLine = allInputLines[currentElement];
                Match currentMatch = validStudent.Match(currentLine);

                if (!string.IsNullOrEmpty(currentLine) && currentMatch.Success)
                {
                    string[] courseInfo = currentLine.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    string course = currentMatch.Groups[1].Value;
                    string student = currentMatch.Groups[2].Value;
                    int mark = int.Parse(currentMatch.Groups[3].Value);

                    if (!studentsByCourse.ContainsKey(course))
                    {
                        studentsByCourse[course] = new Dictionary<string, List<int>>();
                    }

                    if (!studentsByCourse[course].ContainsKey(student))
                    {
                        studentsByCourse[course][student] = new List<int>();
                    }

                    studentsByCourse[course][student].Add(mark);
                }
            }
            
            IsDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data red!");
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (IsDataInitialized)
            {
                if (studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.DisplayException(ExceptionMessages.InexistentCourseInDataBase);
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private static bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InexistentStudentInDataBase);
            }

            return false;
        }
        
        public static void GetStudentScoresFromCourse(string courseName, string userName)
        {
            if (IsQueryForStudentPossible(courseName, userName))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, List<int>>(userName, studentsByCourse[courseName][userName]));
            }
        }

        public static void GetAllStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine(courseName);

                foreach (var studentMarksEntry in studentsByCourse[courseName])
                {
                    OutputWriter.PrintStudent(studentMarksEntry);
                }
            }
        }

        public static void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = studentsByCourse[courseName].Count;
                }

                RepositoryFilters.FilterAndTake(studentsByCourse[courseName], givenFilter, studentsToTake.Value);
            }
        }

        public static void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = studentsByCourse[courseName].Count;
                }

                RepositorySorters.OrderAndTake(studentsByCourse[courseName], comparison, studentsToTake.Value);
            }
        }
    }
}
