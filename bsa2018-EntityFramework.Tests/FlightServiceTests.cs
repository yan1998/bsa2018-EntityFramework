using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.BLL.Services;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using bsa2018_ProjectStructure.DataAccess.Model;
using bsa2018_ProjectStructure.Shared.DTO;
using FakeItEasy;
using NUnit.Framework;

namespace bsa2018_ProjectStructure.BLL.Tests
{
    [TestFixture]
    public class FlightServiceTests
    {
        private List<Flight> Flights { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Flight> fakeFlightRepository;
        private readonly IFlightService flightService;
        private readonly IMapper mapper;

        public FlightServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeFlightRepository = A.Fake<IRepository<Flight>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Flights = new List<Flight>();
            flightService = new FlightService(fakeUnitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeFlightRepository.Create(A<Flight>._)).Invokes((Flight a) => { a.Id = Flights.Count + 1; Flights.Add(a); });
            A.CallTo(() => fakeFlightRepository.Update(A<int>._, A<Flight>._)).Invokes((int id, Flight c) => { Flights.FirstOrDefault(air => air.Id == 1).Destination = c.Destination; });
            A.CallTo(() => fakeUnitOfWork.Flights).Returns(fakeFlightRepository);
        }

        [Test]
        public void AddFlight_When_correct_data_Then_count_equal_1()
        {
            //assign
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 7, 14, 5, 30, 0),
                DeparturePlace = "Odessa, Ukraine",
                Destination = "Vilnius, Lithuania",
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0)
            };

            //act
            flightService.AddFlight(flight);

            //assert
            Assert.That(Flights.Count == 1);
        }

        [Test]
        public void AddFlight_When_DeparturePlace_empty_Then_throws_exception()
        {
            //assign
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 7, 14, 5, 30, 0),
                Destination = "Vilnius, Lithuania",
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0)
            };

            //assert
            Assert.ThrowsAsync<Exception>(()=> flightService.AddFlight(flight));
        }

        [Test]
        public void AddFlight_When_Destination_empty_Then_throws_exception()
        {
            //assign
            FlightDTO flight = new FlightDTO()
            {
                DeparturePlace= "Odessa, Ukraine",
                ArrivalTime = new DateTime(2018, 7, 14, 5, 30, 0),
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0)
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => flightService.AddFlight(flight));
        }

        [Test]
        public void AddFlight_When_DepartureTime_greater_then_ArrivalTime_empty_Then_throws_exception()
        {
            //assign
            FlightDTO flight = new FlightDTO()
            {
                DeparturePlace = "Odessa, Ukraine",
                ArrivalTime = new DateTime(2018, 7, 13, 5, 30, 0),
                DepartureTime = new DateTime(2018, 7, 14, 23, 20, 0)
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => flightService.AddFlight(flight));
        }

        [Test]
        public void UpdateFlight_When_correct_data_Then_Destination_equal_Test()
        {
            //assign
            FlightDTO flight = new FlightDTO()
            {
                ArrivalTime = new DateTime(2018, 7, 14, 5, 30, 0),
                DeparturePlace = "Odessa, Ukraine",
                Destination = "Test",
                DepartureTime = new DateTime(2018, 7, 13, 23, 20, 0)
            };

            //act
            flightService.UpdateFlight(1, flight);

            //assert
            Assert.That(Flights.FirstOrDefault(c => c.Id == 1).Destination == "Test");
        }
    }
}
