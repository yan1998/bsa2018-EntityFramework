using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
            LoadData();
        }

        public DbSet<Aircraft> Aicrafts { get; set; }
        public DbSet<AircraftType> AircraftTypes { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Stewardess> Stewardess { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Crew> Crews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS; Initial catalog=bsa2018_EF");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne<Flight>(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.IdFlight);

            modelBuilder.Entity<Departure>()
                .HasOne<Flight>(d => d.Flight)
                .WithMany(f => f.Departures)
                .HasForeignKey(d => d.IdFlight);

            modelBuilder.Entity<Departure>()
                .HasOne<Crew>(d => d.Crew)
                .WithMany(c => c.Departures)
                .HasForeignKey(d => d.IdCrew);

            modelBuilder.Entity<Departure>()
                .HasOne<Aircraft>(d => d.Aircraft)
                .WithMany(a => a.Departures)
                .HasForeignKey(d => d.IdAircraft);

            modelBuilder.Entity<AircraftType>()
                .HasOne<Aircraft>(at => at.Aircraft)
                .WithOne(a => a.AircraftType)
                .HasForeignKey<Aircraft>(at => at.IdAircraftType);

            modelBuilder.Entity<Crew>()
                .HasOne<Pilot>(c => c.Pilot)
                .WithMany(p => p.Crews)
                .HasForeignKey(c=>c.IdPilot);

            modelBuilder.Entity<StewardessCrew>().HasKey(sc => new { sc.IdStewardess, sc.IdCrew });

            modelBuilder.Entity<StewardessCrew>()
                .HasOne<Stewardess>(sc => sc.Stewardess)
                .WithMany(s => s.StewardessCrews)
                .HasForeignKey(sc => sc.IdStewardess);
            modelBuilder.Entity<StewardessCrew>()
                .HasOne<Crew>(sc => sc.Crew)
                .WithMany(c => c.StewardessCrews)
                .HasForeignKey(sc => sc.IdCrew);
        }

        private void LoadData()
        {
            #region AircraftType

            AircraftType aircraftType1 = new AircraftType
            {
                Id = 1,
                LoadCapacity = 70000,
                Places = 400
            };
            AircraftType aircraftType2 = new AircraftType
            {
                Id = 2,
                LoadCapacity = 2400,
                Places = 114
            };
            AircraftType aircraftType3 = new AircraftType
            {
                Id = 3,
                LoadCapacity = 20000,
                Places = 235
            };
            this.AircraftTypes.AddRange(aircraftType1, aircraftType2, aircraftType3);
            #endregion

            #region Aircrafts

            Aircraft aircraft1 = new Aircraft
            {
                Id = 1,
                Name = "Airbus A330",
                ReleaseDate = new DateTime(2010, 1, 17),
                LifeSpan = new TimeSpan(200, 0, 0, 0),
                IdAircraftType = aircraftType1.Id
            };

            Aircraft aircraft2 = new Aircraft
            {
                Id = 2,
                Name = "Boeing-737",
                ReleaseDate = new DateTime(2009, 6, 7),
                LifeSpan = new TimeSpan(157, 0, 0, 0),
                IdAircraftType = aircraftType2.Id
            };

            Aircraft aircraft3 = new Aircraft
            {
                Id = 3,
                Name = "Boeing-777",
                ReleaseDate = new DateTime(2009, 6, 7),
                LifeSpan = new TimeSpan(157, 0, 0, 0),
                IdAircraftType = aircraftType2.Id
            };
            this.Aicrafts.AddRange( aircraft1, aircraft2, aircraft3);
            #endregion

            #region Pilots

            Pilot pilot1 = new Pilot
            {
                Id = 1,
                Name = "Yan",
                Surname = "Gorshkov",
                Birthday = new DateTime(1998, 8, 21),
                Experience = 2
            };
            Pilot pilot2 = new Pilot
            {
                Id = 2,
                Name = "Vladimir",
                Surname = "Romanov",
                Birthday = new DateTime(1973, 1, 15),
                Experience = 6
            };
            this.Pilots.AddRange(pilot1, pilot2);
            #endregion

            #region Stewardess

            Stewardess stewardess1 = new Stewardess
            {
                Id = 1,
                Name = "Anastasia",
                Surname = "Volkova",
                Birthday = new DateTime(1985, 9, 4)
            };
            Stewardess stewardess2 = new Stewardess
            {
                Id = 2,
                Name = "Anna",
                Surname = "Matveeva",
                Birthday = new DateTime(1992, 3, 28)
            };
            Stewardess stewardess3 = new Stewardess
            {
                Id = 3,
                Name = "Maria",
                Surname = "Mamedova",
                Birthday = new DateTime(1982, 2, 17)
            };
            this.Stewardess.AddRange(stewardess1, stewardess2, stewardess3);
            #endregion

            #region Crews

            Crew crew1 = new Crew
            {
                Id = 1,
                IdPilot = pilot1.Id,
                Pilot = pilot1,
                StewardessCrews = new List<StewardessCrew> {
                    new StewardessCrew{ IdCrew=1, IdStewardess= stewardess1.Id},
                    new StewardessCrew{ IdCrew=1 ,IdStewardess=stewardess2.Id}
                }
            };
            Crew crew2 = new Crew
            {
                Id = 2,
                IdPilot = pilot2.Id,
                Pilot = pilot2,
                StewardessCrews = new List<StewardessCrew> {
                    new StewardessCrew{ IdCrew=2, IdStewardess= stewardess1.Id},
                    new StewardessCrew{ IdCrew=2 ,IdStewardess=stewardess3.Id}
                }
            };
            Crew crew3 = new Crew
            {
                Id = 3,
                IdPilot = pilot2.Id,
                Pilot = pilot2,
                StewardessCrews = new List<StewardessCrew> {
                    new StewardessCrew{ IdCrew=3, IdStewardess= stewardess2.Id},
                    new StewardessCrew{ IdCrew=3, IdStewardess=stewardess3.Id}
                }
            };
            this.Crews.AddRange(crew1, crew2, crew3 );
            #endregion

            #region Flights

            Flight flight1 = new Flight
            {
                Id = 1,
                ArrivalTime = new DateTime(2018, 7, 14, 20, 0, 0),
                DeparturePlace = "Odessa, Ukraine",
                Destination = "Istambul, Turkey",
                DepartureTime = new DateTime(2018, 7, 13, 10, 30, 0)
            };
            Flight flight2 = new Flight
            {
                Id = 2,
                ArrivalTime = new DateTime(2018, 7, 14, 5, 30, 0),
                DeparturePlace = "Odessa, Ukraine",
                Destination = "Vilnius, Lithuania",
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0)
            };
            Flight flight3 = new Flight
            {
                Id = 3,
                ArrivalTime = new DateTime(2018, 7, 15, 22, 40, 0),
                DeparturePlace = "Batumi, Georgia",
                Destination = "Odessa, Ukraine",
                DepartureTime = new DateTime(2018, 7, 15, 14, 0, 0)
            };
            this.Flights.AddRange(flight1, flight2, flight3 );
            #endregion

            #region Tickets
            Ticket ticket1 = new Ticket
            {
                Id = 1,
                Cost = 1000,
                IdFlight = flight1.Id
            };
            Ticket ticket2 = new Ticket
            {
                Id = 2,
                Cost = 1300,
                IdFlight = flight1.Id
            };
            Ticket ticket3 = new Ticket
            {
                Id = 3,
                Cost = 800,
                IdFlight = flight2.Id
            };
            Ticket ticket4 = new Ticket
            {
                Id = 4,
                Cost = 850,
                IdFlight = flight2.Id
            };
            Ticket ticket5 = new Ticket
            {
                Id = 5,
                Cost = 1000,
                IdFlight = flight3.Id
            };
            Ticket ticket6 = new Ticket
            {
                Id = 6,
                Cost = 1100,
                IdFlight = flight3.Id
            };
            this.Tickets.AddRange(ticket1, ticket2, ticket3, ticket4, ticket5, ticket6);
            #endregion

            #region Depatures
            Departure depature1 = new Departure
            {
                Id = 1,
                IdAircraft = aircraft1.Id,
                IdCrew = crew1.Id,
                DepartureTime = new DateTime(2018, 7, 13, 11, 0, 0),
                IdFlight = flight1.Id
            };
            Departure depature2 = new Departure
            {
                Id = 2,
                IdAircraft = aircraft2.Id,
                IdCrew = crew2.Id,
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0),
                IdFlight = flight2.Id
            };
            Departure depature3 = new Departure
            {
                Id = 3,
                IdAircraft = aircraft3.Id,
                IdCrew = crew3.Id,
                DepartureTime = new DateTime(2018, 7, 15, 14, 0, 0),
                IdFlight = flight3.Id
            };
            this.Departures.AddRange(depature1, depature2, depature3);
            #endregion

            SaveChanges();
        }
    }
}
