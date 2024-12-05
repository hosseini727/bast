namespace Raika.Common.SharedInfrastructure.Extension
{
    public static class StringExtensions
    {
        public static string ConvertToEnglishNumbers(this string value)
        {
            if (string.IsNullOrEmpty(value.Trim()))
                return string.Empty;

            char[] chars = value.Trim().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i].Equals('۱'))
                    chars[i] = '1';
                else if (chars[i].Equals('۲'))
                    chars[i] = '2';
                else if (chars[i].Equals('۳'))
                    chars[i] = '3';
                else if (chars[i].Equals('۴'))
                    chars[i] = '4';
                else if (chars[i].Equals('۵'))
                    chars[i] = '5';
                else if (chars[i].Equals('۶'))
                    chars[i] = '6';
                else if (chars[i].Equals('۷'))
                    chars[i] = '7';
                else if (chars[i].Equals('۸'))
                    chars[i] = '8';
                else if (chars[i].Equals('۹'))
                    chars[i] = '9';
                else if (chars[i].Equals('۰'))
                    chars[i] = '0';
            }

            string ss = "";
            for (int i = 0; i < chars.Length; i++)
            {
                ss += chars[i];
            }
            return ss;
        }
        public static string ConvertToFarsiNumber(this string value)
        {
            if (string.IsNullOrEmpty(value.Trim()))
                return string.Empty;

            char[] chars = value.Trim().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i].Equals('1'))
                    chars[i] = '۱';
                else if (chars[i].Equals('2'))
                    chars[i] = '۲';
                else if (chars[i].Equals('3'))
                    chars[i] = '۳';
                else if (chars[i].Equals('4'))
                    chars[i] = '۴';
                else if (chars[i].Equals('5'))
                    chars[i] = '۵';
                else if (chars[i].Equals('6'))
                    chars[i] = '۶';
                else if (chars[i].Equals('7'))
                    chars[i] = '۷';
                else if (chars[i].Equals('8'))
                    chars[i] = '۸';
                else if (chars[i].Equals('9'))
                    chars[i] = '۹';
                else if (chars[i].Equals('0'))
                    chars[i] = '۰';
            }

            string ss = "";
            for (int i = 0; i < chars.Length; i++)
            {
                ss += chars[i];
            }
            return ss;
        }
        public static string ConvertYeAndKe(this string value)
        {
            if (string.IsNullOrEmpty(value.Trim()))
                return string.Empty;

            const char ArabicYeChar = (char)1610;
            const char PersianYeChar = (char)1740;
            const char ArabicKeChar = (char)1603;
            const char PersianKeChar = (char)1705;

            value = value.Trim();
            value = value.ConvertToEnglishNumbers();
            value = value
                .Replace(ArabicYeChar, PersianYeChar)
                .Replace(ArabicKeChar, PersianKeChar);
            return value;
        }
    }
}
