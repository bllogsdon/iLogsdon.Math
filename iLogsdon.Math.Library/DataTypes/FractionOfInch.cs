namespace iLogsdon.Math.DataTypes;

public enum FractionOfInch
{
    Half = 2,
    Quarter = 4,
    Eighth = 8,
    Sixteenth = 16,
    ThirtySecondth = 32,
    SixtyFourth = 64
}

public static class FractionOfInchExtensions
{
    public static int ToValue(this FractionOfInch precision) => (int)precision;

    public static decimal ToDecimal(this FractionOfInch precision) => 1.0m / (int)precision;

    public static double ToDouble(this FractionOfInch precision) => 1.0 / (int)precision;

    public static int Compare(this FractionOfInch value, FractionOfInch compareTo)
    {
        if (value.ToValue() == compareTo.ToValue())
            return 0;
        if (value.ToValue() < compareTo.ToValue())
            return 1;
        return -1;
    }
}