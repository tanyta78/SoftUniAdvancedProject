﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.Exceptions;

namespace BashSoft.IO.Commands
{
   public class OpenFileCommand:Command
    {
        public OpenFileCommand(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager) : base(input, data, judge, repository, inputOutputManager)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length!=2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string fileNAme = this.Data[1];
            Process.Start(SessionData.currentPath + "\\" + fileNAme);
        }
    }
}
