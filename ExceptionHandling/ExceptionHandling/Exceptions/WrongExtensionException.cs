using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Exceptions
{
    public class WrongExtensionException : Exception
    {
        public WrongExtensionException(string message) : base(message)
        {

        }
    }
}
