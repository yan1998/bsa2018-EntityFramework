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
    public class AircraftTypesServiceTests
    {
        private List<AircraftType> AircraftTypes { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<AircraftType> fakeAircraftTypeRepository;
        private readonly IAircraftTypesService aircraftTypesService;
        private readonly IMapper mapper;

        public AircraftTypesServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeAircraftTypeRepository = A.Fake<IRepository<AircraftType>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            AircraftTypes = new List<AircraftType>();
            aircraftTypesService = new AircraftTypesService(fakeUnitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeAircraftTypeRepository.Create(A<AircraftType>._)).Invokes((AircraftType a) => { a.Id = AircraftTypes.Count + 1; AircraftTypes.Add(a); });
            A.CallTo(() => fakeAircraftTypeRepository.Update(A<int>._, A<AircraftType>._)).Invokes((int id, AircraftType a) => { AircraftTypes.FirstOrDefault(air => air.Id == 1).Places = a.Places; });
            A.CallTo(() => fakeUnitOfWork.AircraftTypes).Returns(fakeAircraftTypeRepository);
        }

        [Test]
        public void AddAircraftType_When_correct_data_Then_count_equal_1()
        {
            //assign
            AircraftTypeDTO aircraftType = new AircraftTypeDTO()
            {
                LoadCapacity=100,
                Places=100
            };

            //act
            aircraftTypesService.AddAircraftType(aircraftType);

            //assert
            Assert.That(AircraftTypes.Count == 1);
        }

        [Test]
        public void AddAircraftType_When_places_negative_Then_thow_exception()
        {
            //assign
            AircraftTypeDTO aircraftType = new AircraftTypeDTO()
            {
                LoadCapacity = 100,
                Places = -10
            };

            //assert
            Assert.Throws<Exception>(() => aircraftTypesService.AddAircraftType(aircraftType));
        }

        [Test]
        public void AddAircraftType_When_places_greater_than_1200_Then_thow_exception()
        {
            //assign
            AircraftTypeDTO aircraftType = new AircraftTypeDTO()
            {
                LoadCapacity = 100,
                Places = 15000
            };

            //assert
            Assert.Throws<Exception>(() => aircraftTypesService.AddAircraftType(aircraftType));
        }

        [Test]
        public void AddAircraftType_When_LoadCapacity_negative_Then_thow_exception()
        {
            //assign
            AircraftTypeDTO aircraftType = new AircraftTypeDTO()
            {
                LoadCapacity = -100,
                Places = 100
            };

            //assert
            Assert.Throws<Exception>(() => aircraftTypesService.AddAircraftType(aircraftType));
        }

        [Test]
        public void UpdateAircraftType_When_correct_data_Then_places_equal_200()
        {
            //assign
            AircraftTypeDTO aircraftType = new AircraftTypeDTO()
            {
                LoadCapacity = 100,
                Places = 200
            };

            //act
            aircraftTypesService.UpdateAircraftType(1,aircraftType);

            //assert
            Assert.That(AircraftTypes.FirstOrDefault(a => a.Id == 1).Places == 200);
        }

        [Test]
        public void UpdateAircraftType_When_incorrect_data_Then_throw_exception()
        {
            //assign
            AircraftTypeDTO aircraftType = new AircraftTypeDTO()
            {
                LoadCapacity = 100,
                Places = 12000
            };

            //assert
            Assert.Throws<Exception>(()=> aircraftTypesService.UpdateAircraftType(1, aircraftType));
        }
    }
}
