using System.Collections.Generic;
using System.Threading.Tasks;
using AcsTypes.Error;
using AcsTypes.Types;
using CSharpFunctionalExtensions;
using Domain;
using Services.Models;

namespace Services
{
    public interface ITeamsService
    {
        public Task<Result<IReadOnlyList<Team>, Error>> GetTeamsForMatchType(MatchType matchType);
        public Task<Result<Team, Error>> GetTeam(TeamId teamIdValue);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamSeriesRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamGroundRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamHostCountryRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamOppositionRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamByYearRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamRecordDetails>,Error>> GetTeamBySeasonRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<TeamExtrasDetails>, Error>> GetTeamOverallExtrasRecords(SharedModel sharedServiceModel);
        public Task<Result<IReadOnlyList<InningsExtrasDetails>, Error>> GetTeamInningsExtrasRecords(SharedModel sharedServiceModel);
    }
}