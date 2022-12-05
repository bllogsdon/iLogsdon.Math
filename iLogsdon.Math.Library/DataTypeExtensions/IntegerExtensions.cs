namespace iLogsdon.Math.DataTypeExtensions;

public static class IntegerExtensions
{
    public static decimal ToDecimal(this int value)
    {
        return Convert.ToDecimal(value);
    }

    public static decimal ToDecimalOrDefault(this int value, decimal? defaultValue = null)
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

    public static double ToDouble(this int value)
    {
        return Convert.ToDouble(value);
    }

    public static double ToDoubleOrDefault(this int value, double? defaultValue = null)
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

    public static short ToShort(this int value)
    {
        return Convert.ToInt16(value);
    }

    public static short ToShortOrDefault(this int value, short? defaultValue = null)
    {
        try
        {
            return value.ToShort();
        }
        catch
        {
            return defaultValue ?? default;
        }
    }
}
