using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCardMaker.Operations
{
    public class UnsetPathException : Exception
    {
        public UnsetPathException() : base(string.Format("Path is not set or invalid.")) { }
    }
}
