namespace iLogsdon.Math.DataTypes;

public struct MixedNumber
{
    private long _wholeNumber;
    private Fraction _properFraction;
    private int _sign;

    public long WholeNumber
    {
        get => _sign * _wholeNumber;
        set => SetWholeNumberAndSign(value);
    }

    public Fraction Fraction
    {
        get => _wholeNumber == 0 ? new Fraction(_sign) * _properFraction : _properFraction;
        set
        {
            SetFractionAndSign(value);
            SimplifyFraction();
        }
    }

    public MixedNumber(int wholeNumber, Fraction fraction) : this()
    {
        _sign = 1;
        SetWholeNumberAndSign(wholeNumber);
        SetFractionAndSign(fraction);
        SimplifyFraction();
    }

    public MixedNumber(decimal value, FractionOfInch precision) : this(0, new Fraction(value, precision)) { }

    private void SetWholeNumberAndSign(long value)
    {
        _wholeNumber = System.Math.Abs(value);
        if (value < 0)
            _sign *= -1;
    }

    private void SetFractionAndSign(Fraction fraction)
    {
        _properFraction = new Fraction(System.Math.Abs(fraction.Numerator), fraction.Denominator, fraction.Precision);
        if (fraction.Numerator < 0)
            _sign *= -1;
    }

    private void SimplifyFraction()
    {
        if (_properFraction.IsProperFraction) return;
        FactorImproperFraction();
    }

    private void FactorImproperFraction()
    {
        var integral = _properFraction.Numerator / _properFraction.Denominator;
        var remainder = _properFraction.Numerator % _properFraction.Denominator;

        _wholeNumber += integral;
        _properFraction.Numerator = remainder;
    }

    public decimal ToDecimal()
    {
        return Convert.ToDecimal(WholeNumber) + Fraction.ToDecimal() * Convert.ToDecimal(_sign);
    }

    public double ToDouble()
    {
        return Convert.ToDouble(WholeNumber) + Fraction.ToDouble() * Convert.ToDouble(_sign);
    }

    public override string ToString()
    {
        if (_wholeNumber == 0 && _properFraction.Numerator == 0) return "0";
        var sign = _sign < 0 ? "-" : "";
        if (_wholeNumber == 0) return $"{sign}{_properFraction}";
        if (_properFraction.Numerator == 0) return $"{sign}{_wholeNumber}";
        return $"{sign}{_wholeNumber}-{_properFraction}";
    }

    public static MixedNumber Zero => new(0, Fraction.Zero);
}