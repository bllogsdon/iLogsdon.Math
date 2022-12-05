using iLogsdon.Math.DataTypes;
using iLogsdon.Math.Functions;

namespace iLogsdon.Math.DataTypeExtensions;

public static class DecimalExtensions
{
    public static double ToDouble(this decimal value)
    {
        return Convert.ToDouble(value);
    }

    public static double ToDoubleOrDefault(this decimal value, double? defaultValue = null)
    {
        try
        {
            return value.ToDouble();
        }
        catch
        {
            return defaultValue ?? default;
        }
    }

    public static Fraction ToFraction(this decimal value)
    {
        var (numerator, denominator) = MathFunctions.DecimalToIrreducibleFraction(value);
        return new Fraction(numerator, denominator);
    }

    public static Fraction ToFraction(this decimal value, FractionOfInch precision)
    {
        return new Fraction(value, precision);
    }

    public static decimal ToFractionPart(this decimal value) => System.Math.Abs(value - value.ToIntegralPart());

    public static long ToIntegralPart(this decimal value) => (long)System.Math.Truncate(value);
}
