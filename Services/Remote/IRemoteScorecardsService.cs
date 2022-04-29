using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;

namespace Services.Remote;
    
public interface IRemoteScorecardsService
{
    Task<Result<ScorecardDto, Error>> GetScorecard(int scorecardId);
    Task<Result<ScorecardDto, Error>> GetScorecard(ScorecardSearchTemplate scorecardUrlTemplate);
}