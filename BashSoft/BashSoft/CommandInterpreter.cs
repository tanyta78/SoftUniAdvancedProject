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
                    TryMakeDirectory(data, command);
                    break;
                case "ls":
                   TryTraverseDirectory(data, command);
                    break;
                case "cmp":
                    TryCompareContent(data, command);
                    break;
                case "cdRel":
                    //changeDirRel
                    TryChangeDirectoryRelative(data, command);
                    break;
                case "cdAbs":
                    //changeDirAbs
                    TryChangeDirectoryAbsolute(data, command);
                    break;
                case "readDb":
                    TryReadDataBase(data, command);
                    break;
                case "filter": break;
                case "order": break;
                case "decOrder":break;
                case "download": break;
                case "downloadAsynch": break;
                case "help":
                    GetHelp(); break;
                case "open":
                    TryOpenFile(data, command);
                    break;
                case "show": TryShowWantedData(input,data);
                    break;
                default:DisplayInvalidCommandMessage(input);break;
            }
        }

        private static void TryReadDataBase(string[] data, string command)
        {
            if (data.Length == 2)
            {
                StudentRepository.InitializeData(data[1]);
            }
            else
            {
                DisplayInvalidCommandMessage(command);
            }
        }

        private static void TryChangeDirectoryAbsolute(string[] data, string command)
        {
            if (data.Length == 2)
            {
                IOManager.ChangeCurrentDirectoryAbsolute(data[1]);
            }
            else
            {
                DisplayInvalidCommandMessage(command);
            }
        }

        private static void TryChangeDirectoryRelative(string[] data, string command)
        {
            if (data.Length == 2)
            {
                IOManager.ChangeCurrentDirectoryRelative(data[1]);
            }
            else
            {
                DisplayInvalidCommandMessage(command);
            }
        }

        private static void TryCompareContent(string[] data, string command)
        {
            if (data.Length == 3)
            {
                Tester.CompareContent(data[1], data[2]);
            }
            else
            {
                DisplayInvalidCommandMessage(command);
            }
        }

        private static void TryOpenFile(string[] data, string command)
        {
            if (data.Length == 2)
            {
                Process.Start(SessionData.currentPath + "\\" + $"{data[1]}");
            }
            else
            {
                DisplayInvalidCommandMessage(command);
            }
        }

        private static void TryTraverseDirectory(string[] data, string command)
        {
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
        }

        private static void TryMakeDirectory(string[] data, string command)
        {
            if (data.Length == 2)
            {
                IOManager.CreateDirectoryInCurrentFolder(data[1]);
            }
            else
            {
                DisplayInvalidCommandMessage(command);
            }
        }

        private static void TryShowWantedData(string input, string[] data)
        {
            if (data.Length == 2)
            {
                string courseName = data[1];
                StudentRepository.GetAllStudentsFromCourse(courseName);
            }else if (data.Length == 3)
            {
                string courseName = data[1];
                string student = data[2];
                StudentRepository.GetStudentScoresFromCourse(courseName,student);
            }
            else
            {
                DisplayInvalidCommandMessage(input);
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
