using System.Globalization;

namespace Raika.Common.SharedInfrastructure.DateTimeHelper
{
    public class DateTimeHelper : IDateTimeHelper
    {
        private DateTime PersianDateToGregorian(string persianDate, char separator = '/')
        {
            try
            {
                var index1 = persianDate.IndexOf(separator);
                var index2 = persianDate.IndexOf(separator, index1 + 1);
                var len = persianDate.Length;
                var year = int.Parse(persianDate.Substring(0, index1));
                var month = int.Parse(persianDate.Substring(index1 + 1, index2 - index1 - 1));
                var day = int.Parse(persianDate.Substring(index2 + 1, (len - index2 - 1) <= 2 ? (len - index2 - 1) : 2));
                var pc = new PersianCalendar();
                var dt = new DateTime(year, month, day, pc);
                return dt;
            }
            catch (Exception)
            {
                throw new Exception("The date is invalid.");
            }

        }
        private string GetPersianDate(DateTime date, char separator)
        {
            var pcDate = new PersianCalendar();
            var year = pcDate.GetYear(date);
            var month = pcDate.GetMonth(date);
            var day = pcDate.GetDayOfMonth(date);
            return $"{year:D2}{separator}{month:D2}{separator}{day:D2}";
        }
        private string GetTimeOfDate(DateTime date, bool withSecond = true)
        {
            if (withSecond)
                return $"{date.Hour:D2}:{date.Minute:D2}:{date.Second:D2}";
            else
                return $"{date.Hour:D2}:{date.Minute:D2}";
        }
        public DateTime GetLocalDateTime()
        {
            return DateTime.Now;
        }
        public DateTime GetLocalDate()
        {
            return GetLocalDateTime().Date;
        }
        public string GetFormattedLocalDate()
        {
            var date = GetLocalDateTime();
            return $"{date.Year:D2}/{date.Month:D2}/{date.Day:D2}";
        }
        public TimeSpan GetLocalTime()
        {
            return GetLocalDateTime().TimeOfDay;
        }
        public string GetPersianDate()
        {
            var currentDate = GetLocalDateTime();
            return GetPersianDate(currentDate, '/');
        }
        public string GetPersianDateForFolderName()
        {
            var currentDate = GetLocalDateTime();
            return GetPersianDate(currentDate, '/');
        }
        public string GetPersianDate(DateTime? date)
        {
            if (date != null)
                return GetPersianDate(date.Value, '/');
            return "";
        }
        public string GetPersianDateTime(DateTime? date)
        {
            if (date.HasValue)
                return $"{GetTimeOfDate(date.Value)}-{GetPersianDate(date.Value, '/')}";
            return "";
        }
        public string GetPersianTime()
        {
            DateTime currentDate = GetLocalDateTime();
            return GetTimeOfDate(currentDate);
        }
        public string GetPersianTime(DateTime? date)
        {
            if (date.HasValue)
                return GetTimeOfDate(date.Value);
            else
            {
                var currentDate = GetLocalDateTime();
                return GetTimeOfDate(currentDate);
            }
        }
        public string GetPersianTimeWithoutSecond(DateTime? date)
        {
            if (date.HasValue)
                return GetTimeOfDate(date.Value, withSecond: false);
            else
            {
                var currentDate = GetLocalDateTime();
                return GetTimeOfDate(currentDate, withSecond: false);
            }
        }
        public string GetTimePartInDateTime(DateTime date)
        {
            return GetTimeOfDate(date);
        }
        public DateTime ConvertPersianDateToGregorian(string date)
        {
            return PersianDateToGregorian(date);
        }
        public string GetCurrentYearStartDate()
        {
            var currentDate = GetLocalDateTime();
            string persianYear = GetPersianDate(currentDate).Substring(0, 4);
            return $"{persianYear}/01/01";

        }
        public string GetCurrentYearEndDate()
        {
            var currentDate = GetLocalDateTime();
            var pcDate = new PersianCalendar();
            var persianYear = int.Parse(GetPersianDate(currentDate).Substring(0, 4));
            var isLeap = pcDate.IsLeapYear(persianYear);
            var day = isLeap ? "30" : "29";
            return $"{persianYear}/12/{day}";
        }
    }
}
