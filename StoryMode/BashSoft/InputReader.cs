using System;

namespace BashSoft
{
    public static class InputReader
    {
        private const string EndCommand = "quit";

        public static void StartReadingCommands()
        {
            while (true)
            {
                OutputWriter.WriteMessage($"{SessionData.CurrentPath}> ");
                string input = Console.ReadLine().Trim();

                if (input == EndCommand)
                {
                    break;
                }

                CommandInterpreter.InterpredCommand(input);
            }
        }
    }
}
