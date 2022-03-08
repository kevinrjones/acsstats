using Domain;
using Microsoft.EntityFrameworkCore;

namespace AcsRepository
{
    public class AcsDbContext : DbContext
    {
        public AcsDbContext(DbContextOptions<AcsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MatchRecordDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<TeamRecordDetails>(builder => { builder.HasNoKey(); });
            
            modelBuilder.Entity<PlayerBattingCareerRecordDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<PlayerBowlingCareerRecordDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<PlayerFieldingCareerRecordDetails>(builder => { builder.HasNoKey(); });
            
            modelBuilder.Entity<IndividualBattingDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<IndividualBowlingDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<IndividualFieldingDetails>(builder => { builder.HasNoKey(); });
            
            modelBuilder.Entity<MatchResult>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<TeamExtrasDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<InningsExtrasDetails>(builder => { builder.HasNoKey(); });
            
            modelBuilder.Entity<PartnershipCareerRecordDetails>(builder => { builder.HasNoKey(); });
            modelBuilder.Entity<PartnershipIndividualRecordDetails>(builder => { builder.HasNoKey(); });
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Ground> Grounds { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<GroundsName> GroundsName { get; set; }
        public DbSet<CountryCodes> CountryCodes { get; set; }
        public DbSet<MatchRecordDetails> MatchRecordDetails { get; set; }
        public DbSet<MatchResult> MatchResult { get; set; }
        public DbSet<TeamRecordDetails> TeamRecordDetails { get; set; }
        public DbSet<TeamExtrasDetails> TeamExtrasDetails { get; set; }
        public DbSet<InningsExtrasDetails> InningsExtrasDetails { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<PlayerBattingCareerRecordDetails> PlayerBattingRecordDetails { get; set; }
        public DbSet<PlayerBowlingCareerRecordDetails> PlayerBowlingRecordDetails { get; set; }
        public DbSet<PlayerFieldingCareerRecordDetails> PlayerFieldingRecordDetails { get; set; }
        public DbSet<IndividualFieldingDetails> PlayerIndividualFieldingDetails { get; set; }
        public DbSet<IndividualBattingDetails> PlayerIndividualBattingDetails { get; set; }
        public DbSet<IndividualBowlingDetails> PlayerIndividualBowlingDetails { get; set; }
            
        public DbSet<PartnershipCareerRecordDetails> PartnershipDetails { get; set; }
        public DbSet<PartnershipIndividualRecordDetails> IndividualPartnershipDetails { get; set; }
    }
}