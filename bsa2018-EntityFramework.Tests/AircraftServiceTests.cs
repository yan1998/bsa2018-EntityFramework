using System;
using System.Collections.Generic;
using System.Text;
using bsa2018_ProjectStructure.BLL.Interfaces;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using NUnit.Framework;
using FakeItEasy;
using bsa2018_ProjectStructure.DataAccess.Model;
using System.Linq;
using bsa2018_ProjectStructure.DataAccess.Repository;
using bsa2018_ProjectStructure.BLL.Services;
using AutoMapper;
using bsa2018_ProjectStructure.Shared.DTO;
using System.Threading.Tasks;

namespace bsa2018_ProjectStructure.BLL.Tests
{
    [TestFixture]
    public class AircraftServiceTests
    {
        private List<Aircraft> Aircrafts { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Aircraft> fakeAircraftRepository;
        private readonly IAircraftService aircraftService;
        private readonly IMapper mapper;

        public AircraftServiceTests()
        {
            mapper= new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeAircraftRepository = A.Fake<IRepository<Aircraft>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Aircrafts = new List<Aircraft>();
            aircraftService = new AircraftService(fakeUnitOfWork, mapper );
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeAircraftRepository.Create(A<Aircraft>._)).Invokes((Aircraft a)=> { a.Id=Aircrafts.Count+1; Aircrafts.Add(a); });
            A.CallTo(() => fakeAircraftRepository.Update(A<int>._, A<Aircraft>._)).Invokes((int id ,Aircraft a) => { Aircrafts.FirstOrDefault(air => air.Id == 1).Name = a.Name; });
            A.CallTo(() => fakeUnitOfWork.Aircrafts).Returns(fakeAircraftRepository);
        }

        [Test]
        public void AddAircraft_When_correct_data_Then_count_equal_1()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO()
            {
                IdAircraftType = 1,
                LifeSpan = new TimeSpan(10, 0, 0),
                Name = "Test",
                ReleaseDate = new DateTime(2010, 5, 12)
            };

            //act
            aircraftService.AddAircraft(aircraft);

            //assert
            Assert.That(Aircrafts.Count==1);
        }

        [Test]
        public void AddAircraft_When_short_name_Then_throw_exception()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO()
            {
                IdAircraftType = 1,
                LifeSpan = new TimeSpan(10, 0, 0),
                Name = "T",
                ReleaseDate = new DateTime(2010, 5, 12)
            };

            //assert
            Assert.ThrowsAsync<Exception>(async () => { await aircraftService.AddAircraft(aircraft); });
        }

        [Test]
        public void AddAircraft_When_long_name_Then_throw_exception()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO() {
                IdAircraftType=1,
                LifeSpan=new TimeSpan(10,0,0),
                Name="Tesgfgdgdfgfgdfgdfgdgdfgfdgdgdt",
                ReleaseDate=new DateTime(2010,5,12)
            };

            //assert
            Assert.ThrowsAsync<Exception>(async ()=> { await aircraftService.AddAircraft(aircraft);});
        }

        [Test]
        public void AddAircraft_When_idAircraft_empty_Then_throw_exception()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO()
            {
                LifeSpan = new TimeSpan(10, 0, 0),
                Name = "Tesgfgdgdf",
                ReleaseDate = new DateTime(2010, 5, 12)
            };

            //assert
            Assert.ThrowsAsync<Exception>(async () => { await aircraftService.AddAircraft(aircraft); });
        }

        [Test]
        public void UpdateAircraft_When_correct_data_Then_name_equal_Boeing()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO()
            {
                IdAircraftType = 1,
                LifeSpan = new TimeSpan(10, 0, 0),
                Name = "Boeing",
                ReleaseDate = new DateTime(2010, 5, 12)
            };

            //act
            aircraftService.UpdateAircraft(1,aircraft);

            //assert
            Assert.That(Aircrafts.FirstOrDefault(a=>a.Id==1).Name == "Boeing");
        }

        [Test]
        public void UpdateAircraft_When_long_name_Then_throw_Exception()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO()
            {
                IdAircraftType = 1,
                LifeSpan = new TimeSpan(10, 0, 0),
                Name = "Boeingvfdbvcbxbbvbxbcvbx",
                ReleaseDate = new DateTime(2010, 5, 12)
            };

            //assert
            Assert.ThrowsAsync<Exception>(async()=> { await aircraftService.UpdateAircraft(1, aircraft); });
        }

        [Test]
        public void UpdateAircraft_When_short_name_Then_throw_Exception()
        {
            //assign
            AircraftDTO aircraft = new AircraftDTO()
            {
                IdAircraftType = 1,
                LifeSpan = new TimeSpan(10, 0, 0),
                Name = "f",
                ReleaseDate = new DateTime(2010, 5, 12)
            };

            //assert
            Assert.ThrowsAsync<Exception>(async () => { await aircraftService.UpdateAircraft(1, aircraft); });
        }
    }
}
