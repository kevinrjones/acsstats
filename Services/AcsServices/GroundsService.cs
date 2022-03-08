using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcsRepository;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services.AcsServices
{
    public class GroundsService : IGroundsService
    {
        private readonly IEfUnitOfWork _unitOfWork;
        private readonly ILogger<GroundsService> _logger;

        public GroundsService(IEfUnitOfWork unitOfWork, ILogger<GroundsService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<GroundsWithCodes>, Error>> GetGroundsForMatchType(MatchType matchType)
        {
            var groundsOrError = await _unitOfWork.GroundsRepository.GetGroundsForMatchType(matchType);

            return groundsOrError.Match(Result.Success<IEnumerable<GroundsWithCodes>, Error>
                , Result.Failure<IEnumerable<GroundsWithCodes>, Error>);

        }

        public async Task<Result<Ground, Error>> getGround(int id)
        {
            if (id == 0)
            {
                return new Ground();
            }

            try
            {
                return await _unitOfWork.GroundsRepository.Entities.FirstAsync(g => g.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to get ground for id {Id}", id);
                return Result.Failure<Ground, Error>(Errors.UnexpectedError);
            }
        }
    }
}