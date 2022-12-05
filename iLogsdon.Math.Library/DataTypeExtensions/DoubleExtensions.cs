using iLogsdon.Math.DataTypes;

namespace iLogsdon.Math.DataTypeExtensions;

public static class DoubleExtensions
{
    public static decimal ToDecimal(this double value)
    {
        return Convert.ToDecimal(value);
    }

    public static decimal ToDecimalOrDefault(this double value, decimal? defaultValue = null)
    {
        try
        {
            return value.ToDecimal();
        }
        catch
        {
            return defaultValue ?? default;
        }
    }

    public static Fraction ToFraction(this double value)
    {
        return Convert.ToDecimal(value).ToFraction();
    }

    public static Fraction ToFraction(this double value, FractionOfInch precision)
    {
        return Convert.ToDecimal(value).ToFraction(precision);
    }

    public static long ToIntegralPart(this double value) => Convert.ToDecimal(value).ToIntegralPart();

    public static double ToFractionPart(this double value) => System.Math.Abs(Convert.ToDouble(Convert.ToDecimal(value) - Convert.ToDecimal(value).ToIntegralPart()));
}
