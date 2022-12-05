namespace iLogsdon.Math.DataTypeExtensions;

public static class ShortExtensions
{
    public static int ToInteger(this short value)
    {
        return Convert.ToInt16(value);
    }

    public static int ToInteger(this short? value)
    {
        if (value.HasValue) return value.Value.ToInteger();
        return default;
    }

    public static int ToIntegerOrDefault(this short value, int? defaultValue = null)
    {
        try
        {
            return value.ToInteger();
        }
        catch
        {
            return defaultValue ?? default;
        }
    }

    public static int ToIntegerOrDefault(this short? value, int? defaultValue = null)
    {
        try
        {
            return value.ToInteger();
        }
        catch
        {
            return defaultValue ?? default;
        }
    }
}
