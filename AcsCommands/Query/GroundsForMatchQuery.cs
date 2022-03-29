using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using MediatR;

namespace AcsCommands.Query;

public class GroundsForMatchQuery : IRequest<Result<IReadOnlyList<GroundWithCodeDto>, Error>>
{
    private AcsTypes.Types.MatchType MatchType { get; }

    public GroundsForMatchQuery(AcsTypes.Types.MatchType matchType)
    {
        MatchType = matchType;
    }
}