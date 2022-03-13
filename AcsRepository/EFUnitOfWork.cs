using AcsRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcsRepository
{
    public interface IEfUnitOfWork : IUnitOfWork
    {
        public AcsDbContext DbContext { get; set; }
    }

    public class EfUnitOfWork : IEfUnitOfWork
    {
        public AcsDbContext DbContext { get; set; }
        public IMatchesRepository MatchesRepository { get; }
        public ITeamsRepository TeamsRepository { get; }
        public IGroundsRepository GroundsRepository { get; }
        // public ICountriesRepository CountriesRepository { get; }
        
        public IIndividualBattingDetailsRepository IndividualBattingDetailsRepository { get; }
        public IIndividualBowlingDetailsRepository IndividualBowlingDetailsRepository { get; }
        public IIndividualFieldingDetailsRepository IndividualFieldingDetailsRepository { get; }
        public IPlayerBattingRecordDetailsRepository PlayerBattingRecordDetailsRepository { get; }
        public IPlayerBowlingRecordDetailsRepository PlayerBowlingRecordDetailsRepository { get; }
        public IPlayerFieldingRecordDetailsRepository PlayerFieldingRecordDetailsRepository { get; }
        public IPartnershipRecordDetailsRepository PartnershipDetailsRepository { get; }
        public IMatchRecordDetailsRepository MatchRecordDetailsRepository { get; }
        
        public EfUnitOfWork(AcsDbContext dbCtx, IMatchesRepository matchesRepository, ITeamsRepository teamsRepository, IGroundsRepository groundsRepository, 
            IIndividualBattingDetailsRepository individualBattingDetailsRepository, IIndividualBowlingDetailsRepository individualBowlingDetailsRepository,
            IIndividualFieldingDetailsRepository individualFieldingDetailsRepository, IPlayerBattingRecordDetailsRepository playerBattingRecordDetailsRepository, 
            IPlayerBowlingRecordDetailsRepository playerBowlingRecordDetailsRepository, IPlayerFieldingRecordDetailsRepository playerFieldingRecordDetailsRepository,
            IMatchRecordDetailsRepository matchRecordDetailsRepository, IPartnershipRecordDetailsRepository partnershipDetailsRepository)
        {
            MatchesRepository = matchesRepository;
            TeamsRepository = teamsRepository;
            GroundsRepository = groundsRepository;
            IndividualBattingDetailsRepository = individualBattingDetailsRepository;
            IndividualBowlingDetailsRepository = individualBowlingDetailsRepository;
            IndividualFieldingDetailsRepository = individualFieldingDetailsRepository;
            PlayerBattingRecordDetailsRepository = playerBattingRecordDetailsRepository;
            PlayerBowlingRecordDetailsRepository = playerBowlingRecordDetailsRepository;
            PlayerFieldingRecordDetailsRepository = playerFieldingRecordDetailsRepository;
            MatchRecordDetailsRepository = matchRecordDetailsRepository;
            PartnershipDetailsRepository = partnershipDetailsRepository;
            DbContext = dbCtx;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }



        public void Commit()
        {
            DbContext.SaveChanges();
        }

    }
}
