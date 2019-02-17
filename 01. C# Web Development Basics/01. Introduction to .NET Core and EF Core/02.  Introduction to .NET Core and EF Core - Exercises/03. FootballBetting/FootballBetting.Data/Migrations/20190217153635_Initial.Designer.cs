﻿// <auto-generated />
using System;
using FootballBetting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FootballBetting.Data.Migrations
{
    [DbContext(typeof(FootballBettingDbContext))]
    [Migration("20190217153635_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FootballBetting.Models.Bet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("BetMoney");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("FootballBetting.Models.BetGame", b =>
                {
                    b.Property<int>("BetId");

                    b.Property<int>("GameId");

                    b.Property<int>("ResultPredictionId");

                    b.HasKey("BetId", "GameId");

                    b.HasIndex("GameId");

                    b.HasIndex("ResultPredictionId");

                    b.ToTable("BetGame");
                });

            modelBuilder.Entity("FootballBetting.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("FootballBetting.Models.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompetitionTypeId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CompetitionTypeId");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("FootballBetting.Models.CompetitionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompetitionId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("CompetitionTypes");
                });

            modelBuilder.Entity("FootballBetting.Models.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("FootballBetting.Models.Country", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(3);

                    b.Property<int>("ContinentId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FootballBetting.Models.CountryContinent", b =>
                {
                    b.Property<string>("CountryId");

                    b.Property<int>("ContinentId");

                    b.HasKey("CountryId", "ContinentId");

                    b.HasIndex("ContinentId");

                    b.ToTable("CountryContinent");
                });

            modelBuilder.Entity("FootballBetting.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayGoals");

                    b.Property<int>("AwayTeamId");

                    b.Property<double>("AwayTeamWinBetRate");

                    b.Property<int>("CompetitionId");

                    b.Property<DateTime>("DateTime");

                    b.Property<double>("DrawGameBetRate");

                    b.Property<int>("HomeGoals");

                    b.Property<int>("HomeTeamId");

                    b.Property<double>("HomeTeamWinBetRate");

                    b.Property<int>("RoundId");

                    b.HasKey("Id");

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("RoundId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("FootballBetting.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsInjured");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PositionId");

                    b.Property<int>("SquadNumber");

                    b.Property<int>("TeamId");

                    b.HasKey("Id");

                    b.HasIndex("PositionId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FootballBetting.Models.PlayerStatistic", b =>
                {
                    b.Property<int>("PlayerId");

                    b.Property<int>("GameId");

                    b.Property<int>("PlayedMinutes");

                    b.Property<int>("PlayerAssists");

                    b.Property<int>("ScoredGoals");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("PlayerStatistics");
                });

            modelBuilder.Entity("FootballBetting.Models.Position", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2);

                    b.Property<int>("Description");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("FootballBetting.Models.ResultPrediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Prediction");

                    b.HasKey("Id");

                    b.ToTable("ResultPredictions");
                });

            modelBuilder.Entity("FootballBetting.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name");

                    b.HasKey("Id");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("FootballBetting.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget");

                    b.Property<string>("Logo");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PrimaryKitColorId");

                    b.Property<int>("SecondaryKitColorId");

                    b.Property<int>("TownId");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryKitColorId");

                    b.HasIndex("SecondaryKitColorId");

                    b.HasIndex("TownId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FootballBetting.Models.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("FootballBetting.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Balance");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FootballBetting.Models.Bet", b =>
                {
                    b.HasOne("FootballBetting.Models.User", "User")
                        .WithMany("Bets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Models.BetGame", b =>
                {
                    b.HasOne("FootballBetting.Models.Bet", "Bet")
                        .WithMany("Games")
                        .HasForeignKey("BetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FootballBetting.Models.Game", "Game")
                        .WithMany("Bets")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.ResultPrediction", "ResultPrediction")
                        .WithMany()
                        .HasForeignKey("ResultPredictionId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Models.Competition", b =>
                {
                    b.HasOne("FootballBetting.Models.CompetitionType", "CompetitionType")
                        .WithMany()
                        .HasForeignKey("CompetitionTypeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Models.CompetitionType", b =>
                {
                    b.HasOne("FootballBetting.Models.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId");
                });

            modelBuilder.Entity("FootballBetting.Models.Country", b =>
                {
                    b.HasOne("FootballBetting.Models.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Models.CountryContinent", b =>
                {
                    b.HasOne("FootballBetting.Models.Continent", "Continent")
                        .WithMany("Countries")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.Country", "Country")
                        .WithMany("Continents")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Models.Game", b =>
                {
                    b.HasOne("FootballBetting.Models.Team", "AwayTeam")
                        .WithMany("AwayGames")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.Competition", "Competition")
                        .WithMany("Games")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FootballBetting.Models.Team", "HomeTeam")
                        .WithMany("HomeGames")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.Round", "Round")
                        .WithMany("Games")
                        .HasForeignKey("RoundId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Models.Player", b =>
                {
                    b.HasOne("FootballBetting.Models.Position", "Position")
                        .WithMany("Players")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FootballBetting.Models.PlayerStatistic", b =>
                {
                    b.HasOne("FootballBetting.Models.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FootballBetting.Models.Player", "Player")
                        .WithMany("Games")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Models.Team", b =>
                {
                    b.HasOne("FootballBetting.Models.Color", "PrimaryKitColor")
                        .WithMany()
                        .HasForeignKey("PrimaryKitColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.Color", "SecondaryKitColor")
                        .WithMany()
                        .HasForeignKey("SecondaryKitColorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FootballBetting.Models.Town", "Town")
                        .WithMany("Teams")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FootballBetting.Models.Town", b =>
                {
                    b.HasOne("FootballBetting.Models.Country", "Country")
                        .WithMany("Towns")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
