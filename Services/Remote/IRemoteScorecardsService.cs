using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsTypes.Error;
using CSharpFunctionalExtensions;

namespace Services.Remote;

public interface IRemoteScorecardsService
{
    Task<Result<ScorecardDto, Error>> GetScorecard(int scorecardId);
}