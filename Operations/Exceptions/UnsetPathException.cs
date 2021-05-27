using System;

namespace OpenCardMaker.Operations
{
    public class UnsetPathException : Exception
    {
        public UnsetPathException() : base(string.Format("Path is not set or invalid.")) { }
    }
}
