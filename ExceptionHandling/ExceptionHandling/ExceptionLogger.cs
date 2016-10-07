using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class ExceptionLogger:Logger
    {
        public static string CreateExceptionString(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            CreateExceptionString(sb, e);
            return sb.ToString();
        }

        private static void CreateExceptionString(StringBuilder sb, Exception e)
        {
            sb.AppendFormat($"{Environment.NewLine}Exception Was Found {DateTime.Now}{Environment.NewLine}Type: {e.GetType().FullName}");
            sb.AppendFormat($"{Environment.NewLine}Message: {e.Message}");
            sb.AppendFormat($"{Environment.NewLine}");
        }
    }
}
