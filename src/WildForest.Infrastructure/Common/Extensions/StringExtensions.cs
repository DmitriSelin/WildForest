namespace WildForest.Infrastructure.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceCommaByPeriod(this string value)
        {
            return value.Replace(",", ".");
        }

        public static string ReplacePeriodByComma(this string value)
        {
            return value.Replace(".", ",");
        }
    }
}