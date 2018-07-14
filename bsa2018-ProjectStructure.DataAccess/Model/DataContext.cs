using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace bsa2018_ProjectStructure.DataAccess.Model
{
    public class DataContext : DbContext
    {
        public DataContext() : base()
        {
            if(Database.EnsureCreated())
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
            optionsBuilder.UseSqlServer(@"Server=YAN-PC\SQLEXPRESS;Database=bsa2018_Yan;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasOne<Flight>(t => t.Flight)
                .WithMany(f => f.Tickets)
                .HasForeignKey(t => t.IdFlight)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AircraftType>()
                .HasOne<Aircraft>(at => at.Aircraft)
                .WithOne(a => a.AircraftType)
                .HasForeignKey<Aircraft>(at => at.IdAircraftType)
                .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<Crew>()
                .HasOne<Pilot>(c => c.Pilot)
                .WithMany(p => p.Crews)
                .HasForeignKey(c => c.IdPilot);

            modelBuilder.Entity<StewardessCrew>().HasKey(sc => new { sc.IdStewardess, sc.IdCrew });

            modelBuilder.Entity<StewardessCrew>()
                .HasOne<Stewardess>(sc => sc.Stewardess)
                .WithMany(s => s.StewardessCrews)
                .HasForeignKey(sc => sc.IdStewardess)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<StewardessCrew>()
                .HasOne<Crew>(sc => sc.Crew)
                .WithMany(c => c.StewardessCrews)
                .HasForeignKey(sc => sc.IdCrew)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void LoadData()
        {
            #region AircraftType

            AircraftType aircraftType1 = new AircraftType
            {
                LoadCapacity = 70000,
                Places = 400
            };
            AircraftType aircraftType2 = new AircraftType
            {
                LoadCapacity = 2400,
                Places = 114
            };
            AircraftType aircraftType3 = new AircraftType
            {
                LoadCapacity = 20000,
                Places = 235
            };
            this.AircraftTypes.AddRange(aircraftType1, aircraftType2, aircraftType3);
            #endregion

            #region Aircrafts

            Aircraft aircraft1 = new Aircraft
            {
                Name = "Airbus A330",
                ReleaseDate = new DateTime(2010, 1, 17),
                LifeSpan = new TimeSpan(20, 0, 0),
                AircraftType = aircraftType2
            };

            Aircraft aircraft2 = new Aircraft
            {
                Name = "Boeing-737",
                ReleaseDate = new DateTime(2009, 6, 7),
                LifeSpan = new TimeSpan(17, 0, 0),
                AircraftType = aircraftType2
            };

            Aircraft aircraft3 = new Aircraft
            {
                Name = "Boeing-777",
                ReleaseDate = new DateTime(2009, 6, 7),
                LifeSpan = new TimeSpan(17, 0, 0),
                AircraftType = aircraftType3
            };
            this.Aicrafts.AddRange(aircraft1, aircraft2, aircraft3);
            #endregion

            #region Pilots

            Pilot pilot1 = new Pilot
            {
                Name = "Yan",
                Surname = "Gorshkov",
                Birthday = new DateTime(1998, 8, 21),
                Experience = 2
            };
            Pilot pilot2 = new Pilot
            {
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
                Name = "Anastasia",
                Surname = "Volkova",
                Birthday = new DateTime(1985, 9, 4)
            };
            Stewardess stewardess2 = new Stewardess
            {
                Name = "Anna",
                Surname = "Matveeva",
                Birthday = new DateTime(1992, 3, 28)
            };
            Stewardess stewardess3 = new Stewardess
            {
                Name = "Maria",
                Surname = "Mamedova",
                Birthday = new DateTime(1982, 2, 17)
            };
            this.Stewardess.AddRange(stewardess1, stewardess2, stewardess3);
            #endregion

            #region Crews

            Crew crew1 = new Crew
            {
                Pilot = pilot1,
                StewardessCrews = new List<StewardessCrew> {
                    new StewardessCrew{ Stewardess= stewardess1},
                    new StewardessCrew{ Stewardess=stewardess2}
                }
            };
            Crew crew2 = new Crew
            {
                Pilot = pilot2,
                StewardessCrews = new List<StewardessCrew> {
                    new StewardessCrew{ Stewardess = stewardess1},
                    new StewardessCrew{ Stewardess = stewardess3}
                }
            };
            Crew crew3 = new Crew
            {
                Pilot = pilot2,
                StewardessCrews = new List<StewardessCrew> {
                    new StewardessCrew{ Stewardess= stewardess2},
                    new StewardessCrew{ Stewardess=stewardess3}
                }
            };
            this.Crews.AddRange(crew1, crew2, crew3);
            #endregion

            #region Flights

            Flight flight1 = new Flight
            {
                ArrivalTime = new DateTime(2018, 7, 14, 20, 0, 0),
                DeparturePlace = "Odessa, Ukraine",
                Destination = "Istambul, Turkey",
                DepartureTime = new DateTime(2018, 7, 13, 10, 30, 0)
            };
            Flight flight2 = new Flight
            {
                ArrivalTime = new DateTime(2018, 7, 14, 5, 30, 0),
                DeparturePlace = "Odessa, Ukraine",
                Destination = "Vilnius, Lithuania",
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0)
            };
            Flight flight3 = new Flight
            {
                ArrivalTime = new DateTime(2018, 7, 15, 22, 40, 0),
                DeparturePlace = "Batumi, Georgia",
                Destination = "Odessa, Ukraine",
                DepartureTime = new DateTime(2018, 7, 15, 14, 0, 0)
            };
            this.Flights.AddRange(flight1, flight2, flight3);
            #endregion

            #region Tickets
            Ticket ticket1 = new Ticket
            {
                Cost = 1000,
                Flight = flight1
            };
            Ticket ticket2 = new Ticket
            {
                Cost = 1300,
                Flight = flight1
            };
            Ticket ticket3 = new Ticket
            {
                Cost = 800,
                Flight = flight2
            };
            Ticket ticket4 = new Ticket
            {
                Cost = 850,
                Flight = flight2
            };
            Ticket ticket5 = new Ticket
            {
                Cost = 1000,
                Flight = flight3
            };
            Ticket ticket6 = new Ticket
            {
                Cost = 1100,
                Flight = flight3
            };
            this.Tickets.AddRange(ticket1, ticket2, ticket3, ticket4, ticket5, ticket6);
            #endregion

            //#region Depatures
            //Departure depature1 = new Departure
            //{
            //    Aircraft = aircraft1,
            //    Crew = crew1,
            //    DepartureTime = new DateTime(2018, 7, 13, 11, 0, 0),
            //    Flight = flight1
            //};
            //Departure depature2 = new Departure
            //{
            //    Aircraft = aircraft2,
            //    Crew = crew2,
            //    DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0),
            //    Flight = flight2
            //};
            //Departure depature3 = new Departure
            //{
            //    Aircraft = aircraft3,
            //    Crew = crew3,
            //    DepartureTime = new DateTime(2018, 7, 15, 14, 0, 0),
            //    Flight = flight3
            //};
            //this.Departures.AddRange(depature1, depature2, depature3);
            //#endregion

            SaveChanges();
        }
    }
}
