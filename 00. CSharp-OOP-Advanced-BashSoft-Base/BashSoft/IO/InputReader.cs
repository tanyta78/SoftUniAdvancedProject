namespace BashSoft.IO
{
    using System;
    using BashSoft.Contracts;
    using BashSoft.StaticData;

    public class InputReader : IReader
    {
        private const string EndCommand = "quit";
        private readonly IInterpreter interpreter;

        public InputReader(IInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public void StartReadingCommands()
        {
            OutputWriter.WriteMessage($"{SessionData.currentPath}" + "> ");
            string input = Console.ReadLine().Trim();

            while (input != EndCommand)
            {
                this.interpreter.InterpretCommand(input);
                OutputWriter.WriteMessage($"{SessionData.currentPath}" + "> ");
                input = Console.ReadLine().Trim();
            }
        }
    }
}