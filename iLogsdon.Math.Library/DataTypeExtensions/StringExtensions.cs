using System.Text;

namespace iLogsdon.Math.DataTypeExtensions;

public static class StringExtensions
{
    public static DateTime ToDateTime(this string value)
    {
        return DateTime.Parse(value);
    }

    public static DateTime ToDateTimeOrDefault(this string value, DateTime? defaultValue = null)
    {
        try
        {
            return value.ToDateTime();
        }
        catch
        {
            return defaultValue ?? DateTime.Now;
        }
    }

	public static DateTime ToDateTimeOrUtcNow(this string value)
    {
        return value.ToDateTimeOrDefault(DateTime.UtcNow);
    }

    public static int ToInteger(this string value)
    {
		return int.Parse(value);
    }

    public static int ToIntegerOrDefault(this string value, int? defaultValue = null)
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

    public static string JustDigits(this string value)
    {
        var digitStringBuilder = new StringBuilder();

        foreach(var c in value.ToArray())
        {
            if(!char.IsDigit(c))
                continue;
            digitStringBuilder.Append(c);
        }

        return digitStringBuilder.ToString();
    }

	public static int DigitsToIntegerOrDefault(this string value)
	{
		var justDigits = value.JustDigits();
		return justDigits.ToIntegerOrDefault();
	}
}