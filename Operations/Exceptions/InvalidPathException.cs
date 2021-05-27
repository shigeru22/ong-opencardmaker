using System;

namespace OpenCardMaker.Operations
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException() : base(string.Format("No related files found.")) { }
    }
}
