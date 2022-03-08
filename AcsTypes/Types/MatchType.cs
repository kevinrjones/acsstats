using AcsTypes.Error;
using CSharpFunctionalExtensions;
using LanguageExt;

namespace AcsTypes.Types
{
    public class MatchType: ValueObject<MatchType>
    {
        // todo: create list of possible values 't', 'itt' etc and compare against 'value; in the Create method
        public string Value { get; }

        public MatchType(string value)
        {
            Value = value;
        }

        public static Either<string, MatchType> CreateEx(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return $"Value is invalid {value}";

            return new MatchType(value);
      
        }

        public static Result<MatchType, Error.Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<MatchType, Error.Error>( Errors.InvalidMatchTypeError());
            
            return Result.Success<MatchType, Error.Error>(new MatchType(value));
        }
        
        public static implicit operator string(MatchType type)
        {
            return type.Value;
        }

        public static explicit operator MatchType(string type)
        {
            return Create(type).Value;
        }

        protected override bool EqualsCore(MatchType other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }  
    }
}