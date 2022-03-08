using System;
using CSharpFunctionalExtensions;
using LanguageExt;

namespace AcsTypes.Types
{
    public class MatchResult: ValueObject<MatchResult>
    {
        public int Value { get; }

        private MatchResult(int value)
        {
            Value = value;
        }

        public static Either<string, ScoreLimit> CreateEx(int value)
        {
            if (value < 0 || value >= 2000)
                return $"Value is outside range of 0..2000 {value}";

            return new ScoreLimit(value);
      
        }

        public static Result<MatchResult, Error.Error> Create(int[] value)
        {
            if (value.Length > 4 )
                return Result.Failure<MatchResult, Error.Error>("Too enough entries in array");

            foreach (var val in value)
            {
                if(val != 0 && val != 1 && val != 2 && val != 4 && val != 8)
                    return Result.Failure<MatchResult, Error.Error>("Invalid values for result bitset");
            }

            var bitmap = value.Fold( 0,(init,s) => s | init);
            
            return Result.Success<MatchResult, Error.Error>(new MatchResult(bitmap));
        }

        public static Result<MatchResult, Error.Error> Create(int value)
        {
            if (value < 0 || value > 17 )
                return Result.Failure<MatchResult, Error.Error>("Invalid values for result bitset");
            
            return Result.Success<MatchResult, Error.Error>(new MatchResult(value));
        }

        public static implicit operator int(MatchResult type)
        {
            return type.Value;
        }

        public static explicit operator MatchResult(int type)
        {
            return Create(type).Value;
        }

        protected override bool EqualsCore(MatchResult other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }  
    }
}