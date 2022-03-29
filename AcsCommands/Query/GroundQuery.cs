using AcsRepository.Util;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class GroundQuery : IRequest<Result<GroundDto, Error>>
{
    private int Id { get; }

    public GroundQuery(int id)
    {
        Id = id;
    }
    
    internal class GroundQueryHandler
        : IRequestHandler<GroundQuery, Result<GroundDto, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<GroundQueryHandler> _logger;

        public GroundQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<GroundQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<GroundDto, Error>> Handle(GroundQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT Id, GroundId, KnownAs, CountryId, CountryName, MatchType from grounds where id=@id";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<GroundDto>(sql, new
                {
                    Id = request.Id
                }).First();
                return Result.Success<GroundDto, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {Id}", request.Id);
                return Result.Failure<GroundDto, Error>(Errors.UnexpectedError);
            }
        }
    }
}