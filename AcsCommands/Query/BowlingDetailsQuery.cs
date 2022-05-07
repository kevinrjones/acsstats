using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class BowlingDetailsQuery : IRequest<Result<IReadOnlyList<BowlingDetailsDto>, Error>>
{
    private int PlayerId { get; }

    public BowlingDetailsQuery(int playerId)
    {
        PlayerId = playerId;
    }

    internal class BowlingDetailsQueryHandler
        : IRequestHandler<BowlingDetailsQuery, Result<IReadOnlyList<BowlingDetailsDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<BowlingDetailsQueryHandler> _logger;

        public BowlingDetailsQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<BowlingDetailsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<BowlingDetailsDto>, Error>> Handle(BowlingDetailsQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"select m.Id, 
                       m.MatchType,
                       t.name    as Team,
                       o.name    as opponents,
                       g.KnownAs as Ground,
                       m.MatchStartDate,
                       bd.InningsNumber,
                       bd.Runs,
                       bd.Balls,
                       bd.Wickets,
                       bd.Maidens,
                       bd.Dots,
                       bd.NoBalls,
                       bd.Wides,
                       bd.Fours,
                       bd.Sixes,
                       bd.Captain,
                       m.BallsPerOver
                from BowlingDetails bd
                         join teams t on t.id = bd.TeamId
                         join teams o on o.id = bd.OpponentsId
                         join matches m on m.id = bd.MatchId
                         join Grounds g on g.id = bd.GroundId
                where playerid = @PlayerId
                order by bd.MatchType, m.MatchStartDateAsOffset";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<BowlingDetailsDto>(sql, new
                {
                    request.PlayerId,
                }).ToList();

                return Result.Success<IReadOnlyList<BowlingDetailsDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {Tournament}", request.PlayerId);
                return Result.Failure<IReadOnlyList<BowlingDetailsDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}