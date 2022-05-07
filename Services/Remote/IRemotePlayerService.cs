using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsTypes.Error;
using CSharpFunctionalExtensions;
using Domain;

namespace Services.Remote;

public interface IRemotePlayerService
{
    Task<Result<PlayerBiographyDto, Error>> GetPlayerBiography(int id);
    Task<Result<List<PlayerOverallDto>, Error>> GetPlayerOverall(int id);
    Task<Result<Dictionary<string, List<BattingDetailsDto>>, Error>> GetPlayerBattingDetails(int id);
    Task<Result<Dictionary<string, List<BowlingDetailsDto>>, Error>> GetPlayerBowlingDetails(int id);
}