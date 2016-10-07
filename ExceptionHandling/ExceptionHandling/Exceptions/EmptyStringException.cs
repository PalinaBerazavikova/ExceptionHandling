using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Exceptions
{
    public class EmptyStringException : Exception
    {
        public EmptyStringException(string message) : base(message)
        {

        }
    }
}
