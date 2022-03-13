using System;

namespace AcsRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMatchesRepository MatchesRepository { get; }
        ITeamsRepository TeamsRepository { get; }
        IGroundsRepository GroundsRepository { get; }
        // ICountriesRepository CountriesRepository { get; }
        
        IIndividualBattingDetailsRepository IndividualBattingDetailsRepository { get; }
        IIndividualBowlingDetailsRepository IndividualBowlingDetailsRepository { get; }
        IIndividualFieldingDetailsRepository IndividualFieldingDetailsRepository { get; }
        
        IPlayerBattingRecordDetailsRepository PlayerBattingRecordDetailsRepository { get; }
        IPlayerBowlingRecordDetailsRepository PlayerBowlingRecordDetailsRepository { get; }
        IPlayerFieldingRecordDetailsRepository PlayerFieldingRecordDetailsRepository { get; }
        IPartnershipRecordDetailsRepository PartnershipDetailsRepository { get; }

        IMatchRecordDetailsRepository MatchRecordDetailsRepository { get; }

        void Commit();
    }
    
}