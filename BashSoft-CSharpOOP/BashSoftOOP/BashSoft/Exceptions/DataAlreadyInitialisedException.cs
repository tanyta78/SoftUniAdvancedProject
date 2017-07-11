using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public class DataAlreadyInitialisedException:Exception
    {
        private const string DataAlreadyInitialised = "Data is already initialized!";

        public DataAlreadyInitialisedException():base(DataAlreadyInitialised)
        {
        }

        public DataAlreadyInitialisedException(string message) : base(message)
        {
        }
    }
}
