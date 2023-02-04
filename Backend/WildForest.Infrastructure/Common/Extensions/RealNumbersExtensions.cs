namespace WildForest.Infrastructure.Common.Extensions
{
    public static class RealNumbersExtensions
    {
        public static string RemoveCommaByPeriodToString(this double value)
        {
            return value.ToString().Replace(",", ".");
        }
    }
}