using AcsTypes.Error;
using CSharpFunctionalExtensions;
// ReSharper disable InconsistentNaming

namespace AcsTypes.Types
{
  public class AcsId : ValueObject<AcsId>
  {
    public int Value { get; }

    protected AcsId(int value)
    {
      Value = value;
    }
    
    public static Result<AcsId, Error.Error> Create(int value)
    {
      if (value < 0)
        return Result.Failure<AcsId, Error.Error>(Errors.InvalidIdError(value));

      return Result.Success<AcsId, Error.Error>(new AcsId(value));
    }
    
    public static implicit operator int(AcsId type)
    {
      return type.Value;
    }

    protected override bool EqualsCore(AcsId other)
    {
      return Value == other.Value;
    }

    protected override int GetHashCodeCore()
    {
      return Value.GetHashCode();
    }  
  }

  public sealed class TeamId : AcsId
  {
    private TeamId(int value) : base(value)
    {
    }

    public static explicit operator TeamId(int id)
    {
      return Create(id).Value;
    }

    public new static Result<TeamId, Error.Error> Create(int value)
    {
      if (value < 0)
        return Result.Failure<TeamId, Error.Error>(Errors.InvalidIdError(value));

      return Result.Success<TeamId, Error.Error>(new TeamId(value));
    }
  }

  public sealed class CountryId : AcsId
  {
    private CountryId(int value) : base(value)
    {
    }

    public static explicit operator CountryId(int id)
    {
      return Create(id).Value;
    }

    public new static Result<CountryId, Error.Error> Create(int value)
    {
      if (value < 0)
        return Result.Failure<CountryId, Error.Error>( Errors.InvalidIdError($"CountryId cannot be negative", value));

      return Result.Success<CountryId, Error.Error>(new CountryId(value));
    }
  }
  
  public sealed class VenueId : AcsId
  {
    private VenueId(int value) : base(value)
    {
    }

    public static explicit operator VenueId(int id)
    {
      return Create(id).Value;
    }

    public new static Result<VenueId, Error.Error> Create(int value)
    {
      
      if (value < 0 || value > 7) 
        // Errors.InvalidIdError(value)
        return Result.Failure<VenueId, Error.Error>( Errors.InvalidIdError($"VenueId is not valid", value));

      return Result.Success<VenueId, Error.Error>(new VenueId(value));
    }
  }
  
  public sealed class GroundId : AcsId
  {
    private GroundId(int value) : base(value)
    {
    }
    
    public static explicit operator GroundId(int id)
    {
      return Create(id).Value;
    }


    public new static Result<GroundId,Error.Error> Create(int value)
    {
      if (value < 0)
        return Result.Failure<GroundId, Error.Error>( Errors.InvalidIdError($"GroundId cannot be negative", value));

      return Result.Success<GroundId,Error.Error>(new GroundId(value));
    }
  }
  
  public record RequestDates
  {
    public long StartDateEpoch { get; set; }
    public long EndDateEpoch { get; set; }
  }


  
}