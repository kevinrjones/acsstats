using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class PlayerBiographyQuery : IRequest<Result<PlayerBiographyDto, Error>>
{
    private int PlayerId { get; }

    public PlayerBiographyQuery(int playerId)
    {
        PlayerId = playerId;
    }

    internal class PlayerBiographyQueryHandler
        : IRequestHandler<PlayerBiographyQuery, Result<PlayerBiographyDto, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<PlayerBiographyQueryHandler> _logger;

        public PlayerBiographyQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<PlayerBiographyQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<PlayerBiographyDto, Error>> Handle(PlayerBiographyQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"select FullName, UsedFrom
                        from PlayerNames
                        where PlayerId = @playerid";

            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<NameDetail>(sql, new
                {
                    request.PlayerId,
                }).ToList();

                return Result.Success<PlayerBiographyDto, Error>(new PlayerBiographyDto(result));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {Tournament}", request.PlayerId);
                return Result.Failure<PlayerBiographyDto, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}