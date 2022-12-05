namespace iLogsdon.Math.DataTypeExtensions;

public static class FloatExtensions
{
    public static double ToDouble(this float value)
    {
        return Convert.ToDouble(value);
    }
    
    public static double ToDoubleOrDefault(this float value, double? defaultValue = null)
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
    
    public static decimal ToDecimal(this float value)
    {
        return Convert.ToDecimal(value);
    }

    public static decimal ToDecimalOrDefault(this float value, decimal? defaultValue = null)
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
}
