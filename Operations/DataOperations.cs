using System;
using System.Runtime.CompilerServices;

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
    }
}
