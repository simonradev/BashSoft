namespace BashSoft
{
    public static class ExceptionMessages
    {
        public const string DataAlreadyInitializedException = "Data is already initialized!";
        public const string DataNotInitializedExceptionMessage = "The data structure must be initialized first in order to make any operations with it.";
        public const string InexistentCourseInDataBase = "The course you are trying to get does not exist in the data base!";
        public const string InexistentStudentInDataBase = "Th user name for the student you are trying to get does not exist!";

        public const string InvalidPath = "The folder/file you are trying to acces at the current addres, does not exist.";
        public const string UnauthorizedAccessExceptionMessage = "The folder/file you are trying to get access needs a higher level of rights than you currently have...";
        public const string ComparisonOfFilesWithDifferentSizes = "Files not of equal size, certain missmatch";
        public const string ForbiddenSymbolContainedInName = "The given name contains symbols that are not allowed to be used in names of files and folders.";
        public const string UnableToGoHigherInPartitionHierarchy = "Unable to go higher in the partition hierarchy...";

        public const string UnableToParseNumber = "The second parameter you entered was not recognized as a number...";

        public const string InvalidStudentFilter = "The given filter is not one of th following: excellent/average/poor";
        public const string InvalidComparisonQuery = "The comparison query you want, does not exist in the context of the current program!";
        public const string InvalidTakeCommand = "The take command expected does not match the format wanted!";
        public const string InvalidTakeQuantityParameter = "The quantity parameter you have entered was not recognized as a number!";
    }
}
