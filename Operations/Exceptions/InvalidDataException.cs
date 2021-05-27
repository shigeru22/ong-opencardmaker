using System;

namespace OpenCardMaker.Operations.Exceptions
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException() : base("Invalid data used.") { }
        public InvalidDataException(string message) : base($"Invalid data used: {message}") { }
    }
}