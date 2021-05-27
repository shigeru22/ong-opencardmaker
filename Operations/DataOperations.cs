using System;

namespace OpenCardMaker.Operations
{
    class DataOperations
    {
        public DateTime ParseDateTimeString(string dtStr)
        {
            DateTime temp;

            try
            {
                temp = DateTime.Parse(dtStr);
            }
            catch
            {
                throw new InvalidDateTimeStringException();
            }

            return temp;
        }

        internal static string FileNotFoundText(string path)
        {
            return $"{path} not found.";
        }
    }
}
