using System;

namespace CreditCardGenerator.Extensions
{
    public static class RandomExtensions
    {
        public static DateTime NextDateTime(this Random rnd, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            //Calculate cumulative number of seconds between two DateTimes
            Int32 Days = (dateTimeTo - dateTimeFrom).Days * 60 * 60 * 24;
            Int32 Hours = (dateTimeTo - dateTimeFrom).Hours * 60 * 60;
            Int32 Minutes = (dateTimeTo - dateTimeFrom).Minutes * 60;
            Int32 Seconds = (dateTimeTo - dateTimeFrom).Seconds;

            Int32 range = Days + Hours + Minutes + Seconds;

            //Add random number of seconds to dateTimeFrom
            Int32 NumberOfSecondsToAdd = rnd.Next(range);
            DateTime result = dateTimeFrom.AddSeconds(NumberOfSecondsToAdd);

            //Return the value
            return result;
        }
    }
}

