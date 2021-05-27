using System;

namespace OpenCardMaker.Operations
{
    public class InvalidDateTimeStringException : Exception
    {
        public InvalidDateTimeStringException() : base(string.Format("Invalid DateTime string.")) { }
    }
}
