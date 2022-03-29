using System.Collections.Generic;
using System.Threading.Tasks;
using AcsDto.Dtos;
using AcsDto.Models;
using AcsStatsWeb.Dtos;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface ITeamsService
    {
        public Task<Result<TeamDto, Error>> GetTeam(TeamId teamIdValue);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamSeriesRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamGroundRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamHostCountryRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamOppositionRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamByYearRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetailsDto>,Error>> GetTeamBySeasonRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamExtrasDetailsDto>, Error>> GetTeamOverallExtrasRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<InningsExtrasDetailsDto>, Error>> GetTeamInningsExtrasRecords(SharedModel sharedServiceModel);
    }
}