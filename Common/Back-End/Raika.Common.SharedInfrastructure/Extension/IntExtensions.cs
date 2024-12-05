namespace Raika.Common.SharedInfrastructure.Extension
{
    public static class IntExtensions
    {
        public static string GetFormattedNumber(this int? number)
        {
            return (number is null || number == 0) ? "0" : string.Format("{0:#,#}", number);
        }
    }
}
