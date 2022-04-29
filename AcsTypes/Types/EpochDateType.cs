using System;
using System.Globalization;
using AcsTypes.Error;
using CSharpFunctionalExtensions;

namespace AcsTypes.Types;

public class EpochDateType : ValueObject<EpochDateType>
{
    public long EpochDate { get; }
        
    public EpochDateType(DateTime date)
    {
        EpochDate = (long) (date - new DateTime(1970, 1, 1))
            .TotalSeconds;
    }

    public EpochDateType(long value)
    {
        this.EpochDate = value;
    }

    public static Result<EpochDateType, Error.Error> Create(long value)
    {
        return new EpochDateType(value);
    }

    public static Result<EpochDateType, Error.Error> Create(string value, string format = "dd MMMM yyyy")
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<EpochDateType, Error.Error>(Errors.NullError());

        if (!DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var date))
        {
            return Result.Failure<EpochDateType, Error.Error>(Errors.InvalidDateTypeError());
        }

        return Result.Success<EpochDateType, Error.Error>(new EpochDateType(date));
    }

    protected override bool EqualsCore(EpochDateType other)
    {
        return EpochDate == other.EpochDate;
    }

    protected override int GetHashCodeCore()
    {
        return EpochDate.GetHashCode();
    }
    
    public static implicit operator long(EpochDateType type)
    {
        return type.EpochDate;
    }

}