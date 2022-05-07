using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class BattingDetailsQuery : IRequest<Result<IReadOnlyList<BattingDetailsDto>, Error>>
{
    private int PlayerId { get; }

    public BattingDetailsQuery(int playerId)
    {
        PlayerId = playerId;
    }

    internal class BattingDetailsQueryHandler
        : IRequestHandler<BattingDetailsQuery, Result<IReadOnlyList<BattingDetailsDto>, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<BattingDetailsQueryHandler> _logger;

        public BattingDetailsQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<BattingDetailsQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<BattingDetailsDto>, Error>> Handle(BattingDetailsQuery request,
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
                       bd.Dismissal,
                       bd.DismissalType,
                       bd.FielderId,
                       bd.FielderName,
                       bd.BowlerId,
                       bd.BowlerName,
                       bd.Score,
                       bd.Position,
                       bd.NotOut,
                       bd.Balls,
                       bd.Minutes,
                       bd.Fours,
                       bd.Sixes,
                       bd.Captain,
                       bd.WicketKeeper
                from BattingDetails bd
                         join teams t on t.id = bd.TeamId
                         join teams o on o.id = bd.OpponentsId
                         join matches m on m.id = bd.MatchId
                         join Grounds g on g.id = bd.GroundId
                where playerid = @PlayerId
                order by bd.MatchType, m.MatchStartDateAsOffset";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<BattingDetailsDto>(sql, new
                {
                    request.PlayerId,
                }).ToList();

                return Result.Success<IReadOnlyList<BattingDetailsDto>, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {Tournament}", request.PlayerId);
                return Result.Failure<IReadOnlyList<BattingDetailsDto>, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}