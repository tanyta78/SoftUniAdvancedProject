using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class CommandInterpreter
    {
        public static void InterpredCommand(string input)
        {
            string[] data = input.Split(' ');
            string command = data[0];
            switch (command)
            {
                case "mkdir":
                    if (data.Length == 2)
                    {
                        IOManager.CreateDirectoryInCurrentFolder(data[1]);
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                case "ls":
                    if (data.Length == 1)
                    {
                        IOManager.TraverseDirectory(0);
                    }
                    else if (data.Length == 2)
                    {
                        int depth;
                        bool hasParsed = int.TryParse(data[1], out depth);
                        if (hasParsed)
                        {
                            IOManager.TraverseDirectory(depth);
                        }
                        else
                        {
                            OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                        }
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                case "cmp":
                    if (data.Length == 3)
                    {
                        Tester.CompareContent(data[1],data[2]);
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                case "cdRel":
                    //changeDirRel
                    if (data.Length == 2)
                    {
                        IOManager.ChangeCurrentDirectoryRelative(data[1]);
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                case "cdAbs":
                    //changeDirAbs
                    if (data.Length == 2)
                    {
                        IOManager.ChangeCurrentDirectoryAbsolute(data[1]);
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                case "readDb":
                    if (data.Length == 2)
                    {
                        StudentRepository.InitializeData(data[1]);
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                case "filter": break;
                case "order": break;
                case "decOrder":break;
                case "download": break;
                case "downloadAsynch": break;
                case "help":
                    GetHelp(); break;
                case "open":
                    if (data.Length == 2)
                    {
                        Process.Start(SessionData.currentPath + "\\" + $"{data[1]}");
                    }
                    else
                    {
                        DisplayInvalidCommandMessage(command);
                    }
                    break;
                default:DisplayInvalidCommandMessage(input);break;
            }
        }

        private static void GetHelp()
        {
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
            OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
            OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
            OutputWriter.WriteEmptyLine();
        }

        private static void DisplayInvalidCommandMessage(string command)
        {
            OutputWriter.DisplayException(ExceptionMessages.InvalidCommandParams(command));
        }
    }
}
