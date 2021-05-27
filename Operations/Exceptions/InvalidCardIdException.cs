using System;

namespace OpenCardMaker.Operations
{
    public class InvalidCardIdException : Exception
    {
        public InvalidCardIdException() : base(string.Format("XML with the specified card ID can't be found.")) { }
        public InvalidCardIdException(string path) : base(string.Format(DataOperations.FileNotFoundText(path))) { }
    }
}
