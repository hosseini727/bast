namespace Raika.Common.SharedInfrastructure.DateTimeHelper
{
    public interface IDateTimeHelper
    {
        string GetPersianDate();
        string GetPersianDate(DateTime? date);
        string GetPersianDateTime(DateTime? date);
        string GetPersianTime();
        string GetPersianTime(DateTime? date);
        string GetPersianTimeWithoutSecond(DateTime? date);
        DateTime GetLocalDateTime();
        string GetFormattedLocalDate();
        string GetPersianDateForFolderName();
        string GetTimePartInDateTime(DateTime date);
        DateTime ConvertPersianDateToGregorian(string date);
        string GetCurrentYearStartDate();
        string GetCurrentYearEndDate();
    }
}
