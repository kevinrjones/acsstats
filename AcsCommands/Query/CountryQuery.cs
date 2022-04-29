using AcsDto.Dtos;
using AcsRepository.Util;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace AcsCommands.Query;

public class CountryQuery : IRequest<Result<CountryDto, Error>>
{
    private int Id { get; }

    public CountryQuery(int id)
    {
        Id = id;
    } 
    
    internal class CountryQueryHandler
        : IRequestHandler<CountryQuery, Result<CountryDto, Error>>
    {
        private readonly QueriesConnectionString _queriesConnectionString;
        private readonly ILogger<CountryQueryHandler> _logger;

        public CountryQueryHandler(QueriesConnectionString queriesConnectionString,
            ILogger<CountryQueryHandler> logger)
        {
            _queriesConnectionString = queriesConnectionString;
            _logger = logger;
        }

        public async Task<Result<CountryDto, Error>> Handle(CountryQuery request,
            CancellationToken cancellationToken)
        {
            string sql =
                @"SELECT Id, CountryName as Name, MatchType
                    FROM Grounds 
                    WHERE CountryId = @Id
                    LIMIT 1";
            try
            {
                await using var connection = new MySqlConnection(_queriesConnectionString.Value);
                var result = connection.Query<CountryDto>(sql, new
                {
                    Id = request.Id
                }).First();
                return Result.Success<CountryDto, Error>(result);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process this request: {Id}", request.Id);
                return Result.Failure<CountryDto, Error>(Errors.GetUnexpectedError(e.Message));
            }
        }
    }
}