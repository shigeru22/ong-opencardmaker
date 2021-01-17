using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCardMaker.Operations
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException() : base(string.Format("No related files found.")) { }
    }
}
