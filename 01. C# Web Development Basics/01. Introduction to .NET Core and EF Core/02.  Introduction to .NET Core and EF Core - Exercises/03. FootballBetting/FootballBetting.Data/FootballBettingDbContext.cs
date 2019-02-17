namespace FootballBetting.Data
{
    using FootballBetting.Models;
    using Microsoft.EntityFrameworkCore;

    public class FootballBettingDbContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<CountryContinent> CountryContinent { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }

        public DbSet<Competition> Competitions { get; set; }

        public DbSet<CompetitionType> CompetitionTypes { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<BetGame> BetGame { get; set; }

        public DbSet<ResultPrediction> ResultPredictions { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(@"Server=.;Database=FootballBetting;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Team>()
                .HasOne(x => x.PrimaryKitColor)
                .WithMany()
                .HasForeignKey(x => x.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder
               .Entity<Team>()
              .HasOne(x => x.SecondaryKitColor)
              .WithMany()
              .HasForeignKey(x => x.SecondaryKitColorId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Team>()
                .HasOne(x => x.Town)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.TownId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Town>()
                .HasOne(x => x.Country)
                .WithMany(x => x.Towns)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<CountryContinent>()
                .HasKey(x => new { x.CountryId, x.ContinentId });

            modelBuilder
                .Entity<Country>()
                .HasMany(x => x.Continents)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Continent>()
                .HasMany(x => x.Countries)
                .WithOne(x => x.Continent)
                .HasForeignKey(x => x.ContinentId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Player>()
                .HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId);

            modelBuilder
                .Entity<Player>()
                .HasOne(x => x.Position)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<PlayerStatistic>()
                .HasKey(x => new { x.PlayerId, x.GameId });

            modelBuilder
                .Entity<Player>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Game>()
                .HasMany(x => x.Players)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId);

            modelBuilder
                .Entity<Game>()
                .HasOne(x => x.Round)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.RoundId);

            modelBuilder
                .Entity<Game>()
                .HasOne(x => x.Competition)
                .WithMany(x => x.Games)
                .HasForeignKey(x => x.CompetitionId);

            modelBuilder
                .Entity<Game>()
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Game>()
                .HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Competition>()
                .HasOne(x => x.CompetitionType)
                .WithMany()
                .HasForeignKey(x => x.CompetitionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<BetGame>()
                .HasKey(x => new { x.BetId, x.GameId });

            modelBuilder
                .Entity<Bet>()
                .HasMany(x => x.Games)
                .WithOne(x => x.Bet)
                .HasForeignKey(x => x.BetId);

            modelBuilder
                .Entity<Game>()
                .HasMany(x => x.Bets)
                .WithOne(x => x.Game)
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<BetGame>()
                .HasOne(x => x.ResultPrediction)
                .WithMany()
                .HasForeignKey(x => x.ResultPredictionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Bet>()
                .HasOne(x => x.User)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.UserId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
