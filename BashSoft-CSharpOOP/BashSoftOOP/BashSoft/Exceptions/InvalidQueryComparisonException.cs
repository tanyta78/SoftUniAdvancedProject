using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
   public class InvalidQueryComparisonException:Exception
    {
        private const string InvalidQueryComparison =
            "The comparison query you want, does not exist in the context of the current program!";

        public InvalidQueryComparisonException():base(InvalidQueryComparison)
        {
        }

        public InvalidQueryComparisonException(string message) : base(message)
        {
        }
    }
}
