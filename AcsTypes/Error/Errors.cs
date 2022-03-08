using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
// ReSharper disable InconsistentNaming

namespace AcsTypes.Error;

public static partial class F
{
    public static Error Error(string message) => new Error(message);
}

public class Error : ICombine
{
    public virtual string Message { get; }
    public override string ToString() => Message;
    
    public ICombine Combine(ICombine value)
    {
        var other = value as Error;

        if (other == null) return this;

        return new Error(this.Message + "; " + other.Message);
    }

    protected Error()
    {
    }

    internal Error(string Message)
    {
        this.Message = Message;
    }

    public static implicit operator Error(string m) => new Error(m);
}

public static class Errors
{
    public static UnexpectedError UnexpectedError
        => new UnexpectedError();

    public static InvalidId InvalidIdError(int id)
        => new InvalidId(id);

    public static InvalidDate InvalidDateError(string startDate, string endDate)
        => new InvalidDate(startDate, endDate);

    public static InvalidDate InvalidDateError(string date)
        => new InvalidDate(date);

    public static InvalidId InvalidIdError(string message,int id)
        => new InvalidId(message, id);

    public static InvalidMatchType InvalidMatchTypeError()
        => new InvalidMatchType();

    public static HttpError HttpError(Exception e)
        => new HttpError(e);

}

public sealed class UnexpectedError : Error
{
    public override string Message { get; }
        = "An unexpected error has occurred";
}

public sealed class InvalidId : Error
{
    int Id { get; }

    public InvalidId(int id)
    {
        Id = id;
        Message = $"Unable to create an AcsId with id {Id}";
    }

    public InvalidId(string message, int id)
    {
        Id = id;
        Message = $"{message} {Id}";
    }

    public override string Message { get; }
}

public sealed class InvalidDate : Error
{
    string date { get; }

    public InvalidDate(string date)
    {
        this.date = date;
        Message = $"Unable to a date with value {this.date}";
    }

    public InvalidDate(string startDate, string endDate)
    {
        Message = $"End date {endDate} cannot be before start date {startDate}";
    }


    public override string Message { get; }
}
public sealed class InvalidMatchType : Error
{

    public override string Message
        => $"You must specify a match/tournament type";
}

public sealed class HttpError : Error
{
    private readonly Exception _e;

    public HttpError(Exception e)
    {
        _e = e;
    }
    
    public override string Message
        => $"You must specify a match/tournament type {_e.Message}";
}
