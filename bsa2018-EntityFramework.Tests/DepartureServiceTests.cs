using AutoMapper;
using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.BLL.Services;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using bsa2018_ProjectStructure.DataAccess.Model;
using bsa2018_ProjectStructure.Shared.DTO;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace bsa2018_ProjectStructure.BLL.Tests
{
    [TestFixture]
    public class DepartureServiceTests
    {
        private List<Departure> Departures { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Departure> fakeDepartureRepository;
        private readonly IDepartureService departureService;
        private readonly IMapper mapper;

        public DepartureServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeDepartureRepository = A.Fake<IRepository<Departure>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Departures = new List<Departure>();
            departureService = new DepartureService(fakeUnitOfWork, mapper);
        }

        [SetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeDepartureRepository.Create(A<Departure>._)).Invokes((Departure d) => { d.Id = Departures.Count + 1; Departures.Add(d); });
            A.CallTo(() => fakeDepartureRepository.Update(A<int>._, A<Departure>._)).Invokes((int id, Departure d) => { Departures.FirstOrDefault(dep => dep.Id == 1).IdFlight= d.IdFlight; });
            A.CallTo(() => fakeUnitOfWork.Departures).Returns(fakeDepartureRepository);
        }

        [Test]
        public void AddDeparture_When_correct_data_Then_count_equal_1()
        {
            //assign
            DepartureDTO departure = new DepartureDTO()
            {
                FlightNumber=1,
                DepartureTime=new DateTime(2018,2,2),
                IdAircraft=1,
                IdCrew=1
            };

            //act
            departureService.AddDeparture(departure);

            //assert
            Assert.That(Departures.Count == 1);
        }

        [Test]
        public void AddDeparture_When_IdCrew_empty_Then_throws_exception()
        {
            //assign
            DepartureDTO departure = new DepartureDTO()
            {
                FlightNumber = 1,
                DepartureTime = new DateTime(2018, 2, 2),
                IdAircraft = 1
            };

            //assert
            Assert.Throws<Exception>(() => departureService.AddDeparture(departure));
        }

        [Test]
        public void AddDeparture_When_FlightNumber_empty_Then_throws_exception()
        {
            //assign
            DepartureDTO departure = new DepartureDTO()
            {
                IdCrew = 1,
                DepartureTime = new DateTime(2018, 2, 2),
                IdAircraft = 1
            };

            //assert
            Assert.Throws<Exception>(() => departureService.AddDeparture(departure));
        }

        [Test]
        public void AddDeparture_When_IdAircraft_empty_Then_throws_exception()
        {
            //assign
            DepartureDTO departure = new DepartureDTO()
            {
                FlightNumber = 1,
                DepartureTime = new DateTime(2018, 2, 2),
                IdCrew = 1
            };

            //assert
            Assert.Throws<Exception>(() => departureService.AddDeparture(departure));
        }

        [Test]
        public void UpdateDeparture_When_correct_data_Then_IdFight_equal_2()
        {
            //assign
            DepartureDTO departure = new DepartureDTO()
            {
                FlightNumber = 2,
                DepartureTime = new DateTime(2018, 2, 2),
                IdAircraft = 1,
                IdCrew = 1
            };

            //act
            departureService.UpdateDeparture(1, departure);

            //assert
            Assert.That(Departures.FirstOrDefault(c => c.Id == 1).IdFlight == 2);
        }

        [Test]
        public void UpdateDeparture_When_incorrect_data_Then_throws_exception()
        {
            //assign
            DepartureDTO departure = new DepartureDTO()
            {
                FlightNumber = 2,
                DepartureTime = new DateTime(2018, 2, 2),
                IdAircraft = 1,
            };

            //assert
            Assert.Throws<Exception>(() => departureService.UpdateDeparture(1, departure));
        }
    }
}
