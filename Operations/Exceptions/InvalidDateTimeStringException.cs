using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCardMaker.Operations
{
    public class InvalidDateTimeStringException : Exception
    {
        public InvalidDateTimeStringException() : base(string.Format("Invalid DateTime string.")) { }
    }
}
