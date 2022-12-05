using iLogsdon.Math.DataTypeExtensions;

namespace iLogsdon.Math.Functions;

public static class MathFunctions
{
    public static long GreatestCommonDivisor(long value1, long value2)
    {
        return value2 == 0 ? value1 : GreatestCommonDivisor(value2, value1 % value2);
    }

    public static long LeastCommonDivisor(long value1, long value2)
    {
        return value1 * value2 / GreatestCommonDivisor(value1, value2);
    }

    public static (long numerator, long denominator) DecimalToIrreducibleFraction(decimal value)
    {
        var integral = value.ToIntegralPart();
        var fraction = value.ToFractionPart();
        const long maxPrecision = 1000000000000000000;
        var gcd = GreatestCommonDivisor((long)System.Math.Round(fraction * maxPrecision), maxPrecision);

        var numerator = (long)System.Math.Round(fraction * maxPrecision) / gcd;
        var denominator = maxPrecision / gcd;

        return ((integral * denominator) + numerator, denominator);
    }

}