using CSharpFunctionalExtensions;
using LanguageExt;

namespace AcsTypes.Types
{
    public class ScoreLimit : ValueObject<ScoreLimit>
    {
        public int Value { get; }

        public ScoreLimit(int value)
        {
            Value = value;
        }

        public static Either<string, ScoreLimit> CreateEx(int value)
        {
            if (value < 0 || value >= 2000)
                return $"Value is outside range of 0..2000 {value}";

            return new ScoreLimit(value);
        }

        public static Result<ScoreLimit, Error.Error> Create(int value)
        {
            if (value < 0)
                return Result.Failure<ScoreLimit, Error.Error>("Limit is too low");

            return Result.Success<ScoreLimit, Error.Error>(new ScoreLimit(value));
        }

        public static implicit operator int(ScoreLimit type)
        {
            return type.Value;
        }

        public static explicit operator ScoreLimit(int value)
        {
            return ScoreLimit.Create(value).Value;
        }

        protected override bool EqualsCore(ScoreLimit other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}