using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCardMaker.Operations
{
    public class InvalidCardIdException : Exception
    {
        public InvalidCardIdException() : base(string.Format("XML with the specified card ID can't be found.")) { }
        public InvalidCardIdException(string path) : base(string.Format(DataOperations.FileNotFoundText(path))) { }
    }
}
