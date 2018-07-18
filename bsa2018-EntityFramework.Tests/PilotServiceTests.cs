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
    public class PilotServiceTests
    {
        private List<Pilot> Pilots { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Pilot> fakePilotRepository;
        private readonly IPilotService pilotService;
        private readonly IMapper mapper;

        public PilotServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakePilotRepository = A.Fake<IRepository<Pilot>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Pilots = new List<Pilot>();
            pilotService = new PilotService(fakeUnitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakePilotRepository.Create(A<Pilot>._)).Invokes((Pilot a) => { a.Id = Pilots.Count + 1; Pilots.Add(a); });
            A.CallTo(() => fakePilotRepository.Update(A<int>._, A<Pilot>._)).Invokes((int id, Pilot c) => { Pilots.FirstOrDefault(air => air.Id == 1).Name = c.Name; });
            A.CallTo(() => fakeUnitOfWork.Pilots).Returns(fakePilotRepository);
        }

        [Test]
        public void AddPilot_When_correct_data_Then_count_equal_1()
        {
            //assign
            PilotDTO pilot = new PilotDTO()
            {
                Name="Yan",
                Surname="Gorshkov",
                Birthday=new DateTime(1998,8,21),
                Experience=5
            };

            //act
            pilotService.AddPilot(pilot);

            //assert
            Assert.That(Pilots.Count == 1);
        }

        [Test]
        public void AddPilot_When_empty_name_Then_throws_exception()
        {
            //assign
            PilotDTO pilot = new PilotDTO()
            {
                Surname = "Gorshkov",
                Birthday = new DateTime(1998, 8, 21),
                Experience = 5
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => pilotService.AddPilot(pilot));
        }

        [Test]
        public void AddPilot_When_empty_surname_Then_throws_exception()
        {
            //assign
            PilotDTO pilot = new PilotDTO()
            {
                Name = "Yan",
                Birthday = new DateTime(1998, 8, 21),
                Experience = 5
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => pilotService.AddPilot(pilot));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void AddPilot_When_incorrect_Experience_Then_throws_exception(int experience)
        {
            //assign
            PilotDTO pilot = new PilotDTO()
            {
                Name = "Yan",
                Surname="Gorshkov",
                Birthday = new DateTime(1998, 8, 21),
                Experience = experience
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => pilotService.AddPilot(pilot));
        }

        [Test]
        public void UpdatePilot_When_correct_data_Then_Name_equal_Test()
        {
            //assign
            PilotDTO pilot = new PilotDTO()
            {
                Name = "Test",
                Surname = "Gorshkov",
                Birthday = new DateTime(1998, 8, 21),
                Experience = 5
            };

            //act
            pilotService.UpdatePilot(1, pilot);

            //assert
            Assert.That(Pilots.FirstOrDefault(c => c.Id == 1).Name == "Test");
        }
    }
}
