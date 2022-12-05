using iLogsdon.Math.DataTypeExtensions;
using iLogsdon.Math.Functions;

namespace iLogsdon.Math.DataTypes;

public struct Fraction : IEquatable<Fraction>
{
    private long _numerator;
    private long _denominator;
    private FractionOfInch _precision;

    public long Numerator
    {
        get => _numerator;
        set
        {
            _numerator = value;
            Reduce();
        }
    }

    public long Denominator
    {
        get => _denominator;
        set
        {
            _denominator = value;
            CheckIfDenominatorZero();
            Reduce();
        }
    }

    public FractionOfInch Precision
    {
        get => _precision;
        set
        {
            _precision = value;
            Reduce();
        }
    }

    public bool IsProperFraction => Numerator != 0 && System.Math.Abs(Numerator) < Denominator;

    public Fraction(long numerator, long denominator, FractionOfInch precision)
    {
        _numerator = numerator;
        _denominator = denominator;
        _precision = precision;
        CheckIfDenominatorZero();
        Reduce();
    }

    public Fraction(long numerator, long denominator) : this(numerator, denominator, FractionOfInch.Sixteenth) { }

    public Fraction(long numerator, FractionOfInch precision) : this(numerator, 1, precision) { }

    public Fraction(long numerator) : this(numerator, 1) { }

    public Fraction(decimal value, FractionOfInch precision)
    {
        var (numerator, denominator) = MathFunctions.DecimalToIrreducibleFraction(value);
        _numerator = numerator;
        _denominator = denominator;
        _precision = precision;
        CheckIfDenominatorZero();
        Reduce();
    }

    private void CheckIfDenominatorZero()
    {
        if (_denominator != 0)
            return;
        _denominator = 1;
        throw new InvalidOperationException("Denominator cannot be 0.");
    }

    private void Reduce()
    {
        var value = ToDecimal();
        var (numerator, denominator) = Reduce(value, Precision);
        _numerator = numerator;
        _denominator = denominator;
    }

    public decimal ToDecimal()
    {
        return Numerator / Convert.ToDecimal(Denominator);
    }

    public double ToDouble()
    {
        return Numerator / Convert.ToDouble(Denominator);
    }

    public override string ToString()
    {
        return Numerator == 0 ? "0" : $"{Numerator}/{Denominator}";
    }

    public Fraction ToReciprocal()
    {
        if (_numerator == 0) return new Fraction(0, 1, _precision);

        var numerator = _denominator;
        var denominator = _numerator;

        return new Fraction(numerator, denominator, _precision);
    }

    public static Fraction Zero => new(0);

    public static Fraction operator +(Fraction value1)
    {
        value1.Numerator *= 1;
        return value1;
    }

    public static Fraction operator -(Fraction value1)
    {
        value1.Numerator *= -1;
        return value1;
    }

    public static Fraction operator ++(Fraction value1)
    {
        value1.Numerator += value1.Denominator;
        return value1;
    }

    public static Fraction operator --(Fraction value1)
    {
        value1.Numerator -= value1.Denominator;
        return value1;
    }

    public static Fraction operator +(Fraction value1, Fraction value2)
    {
        var numerator = (value1.Numerator * value2.Denominator) + (value2.Numerator * value1.Denominator);
        var denominator = value1.Denominator * value2.Denominator;

        return new Fraction(numerator, denominator, value1.Precision.Compare(value1.Precision) >= 0 ? value1.Precision : value2.Precision);
    }

    public static Fraction operator -(Fraction value1, Fraction value2)
    {
        var numerator = (value1.Numerator * value2.Denominator) - (value2.Numerator * value1.Denominator);
        var denominator = value1.Denominator * value2.Denominator;

        return new Fraction(numerator, denominator, value1.Precision.Compare(value1.Precision) >= 0 ? value1.Precision : value2.Precision);
    }

    public static Fraction operator *(Fraction value1, Fraction value2)
    {
        var numerator = value1.Numerator * value2.Numerator;
        var denominator = value1.Denominator * value2.Denominator;

        return new Fraction(numerator, denominator, value1.Precision.Compare(value2.Precision) >= 0 ? value1.Precision : value2.Precision);
    }

    public static Fraction operator /(Fraction value1, Fraction value2)
    {
        return value1 * value2.ToReciprocal();
    }

    public static bool operator ==(Fraction value1, Fraction value2)
    {
        var numerator1 = value1.Numerator * value2.Denominator;
        var numerator2 = value2.Numerator * value1.Denominator;

        return numerator1 == numerator2;
    }

    public static bool operator !=(Fraction value1, Fraction value2)
    {
        var numerator1 = value1.Numerator * value2.Denominator;
        var numerator2 = value2.Numerator * value1.Denominator;

        return numerator1 != numerator2;
    }

    public static bool operator <=(Fraction value1, Fraction value2)
    {
        var numerator1 = value1.Numerator * value2.Denominator;
        var numerator2 = value2.Numerator * value1.Denominator;

        return numerator1 <= numerator2;
    }

    public static bool operator >=(Fraction value1, Fraction value2)
    {
        var numerator1 = value1.Numerator * value2.Denominator;
        var numerator2 = value2.Numerator * value1.Denominator;

        return numerator1 >= numerator2;
    }

    public static bool operator <(Fraction value1, Fraction value2)
    {
        var numerator1 = value1.Numerator * value2.Denominator;
        var numerator2 = value2.Numerator * value1.Denominator;

        return numerator1 < numerator2;
    }

    public static bool operator >(Fraction value1, Fraction value2)
    {
        var numerator1 = value1.Numerator * value2.Denominator;
        var numerator2 = value2.Numerator * value1.Denominator;

        return numerator1 > numerator2;
    }

    private static (long Numerator, long Denominator) Reduce(decimal value, FractionOfInch precision)
    {
        var sign = value < 0 ? -1 : 1;
        var integralValue = System.Math.Abs(value.ToIntegralPart());
        var fractionValue = System.Math.Abs(value.ToFractionPart());
        long numerator;
        long denominator;

        if (fractionValue == 0)
        {
            numerator = integralValue;
            denominator = 1;
        }
        else
        {
            numerator = (long)System.Math.Round(fractionValue * precision.ToValue(), MidpointRounding.AwayFromZero);
            numerator += integralValue * precision.ToValue();
            denominator = precision.ToValue();

            var gcd = MathFunctions.GreatestCommonDivisor(numerator, denominator);

            numerator /= gcd;
            denominator /= gcd;
        }

        return (numerator * sign, denominator);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj is Fraction fraction ? fraction : default);
    }

    public bool Equals(Fraction fraction)
    {
        return Numerator == fraction.Numerator &&
                Denominator == fraction.Denominator &&
                Precision == fraction.Precision;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator, Precision);
    }
}