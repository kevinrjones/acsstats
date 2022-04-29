using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace AcsTypes.Types;

public class ScorecardSearchTemplate : ValueObject
{
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public string Date { get; set; }

    private ScorecardSearchTemplate(string homeTeam, string awayTeam, string date)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Date = date;
    }

    public static Result<ScorecardSearchTemplate, Error.Error> Create(string value)
    {
        // name-v-name-date
        var values = value.Split("-");
        if (values.Length != 4 || values[1] != "v")
        {
            return Result.Failure<ScorecardSearchTemplate, Error.Error>(
                "The template is not in the correct format, the format should be 'name-v-name-date' where name and date are values, 'v' and '-' are constants");
        }

        return Result.Success<ScorecardSearchTemplate, Error.Error>(new ScorecardSearchTemplate(values[0], values[2], values[3]));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return HomeTeam;
        yield return AwayTeam;
        yield return Date;
    }

}