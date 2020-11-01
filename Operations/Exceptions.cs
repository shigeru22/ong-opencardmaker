using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCardMaker.Operations
{
    public class InvalidDateTimeStringException : Exception
    {
        public InvalidDateTimeStringException() : base(string.Format("Invalid DateTime string.")) { }
    }

    public class UnsetPathException : Exception
    {
        public UnsetPathException() : base(string.Format("Path is not set or invalid.")) { }
    }

    public class InvalidPathException : Exception
    {
        public InvalidPathException() : base(string.Format("No related files found.")) { }
    }

    public class InvalidCardIdException : Exception
    {
        public InvalidCardIdException() : base(string.Format("XML with the specified card ID can't be found.")) { }
        public InvalidCardIdException(string path) : base(string.Format(CustomText.FileNotFoundText(path))) { }
    }

    class CustomText
    {
        internal static string FileNotFoundText(string path)
        {
            return $"{path} not found.";
        }
    }
}
