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
    public class CrewServiceTests
    {
        private List<Crew> Crews { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Crew> fakeCrewRepository;
        private readonly ICrewService crewService;
        private readonly IMapper mapper;

        public CrewServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeCrewRepository = A.Fake<IRepository<Crew>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Crews = new List<Crew>();
            crewService = new CrewService(fakeUnitOfWork, mapper);
        }

        [SetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeCrewRepository.Create(A<Crew>._)).Invokes((Crew a) => { a.Id = Crews.Count + 1; Crews.Add(a); });
            A.CallTo(() => fakeCrewRepository.Update(A<int>._, A<Crew>._)).Invokes((int id, Crew c) => { Crews.FirstOrDefault(air => air.Id == 1).IdPilot = c.IdPilot; });
            A.CallTo(() => fakeUnitOfWork.Crews).Returns(fakeCrewRepository);
        }

        [Test]
        public void AddCrew_When_correct_data_Then_count_equal_1()
        {
            //assign
            CrewDTO crew = new CrewDTO()
            {
                IdPilot=1,
                idStewardess=new int[] { 1,2,3}
            };

            //act
            crewService.AddCrew(crew);

            //assert
            Assert.That(Crews.Count == 1);
        }

        [Test]
        public void AddCrew_When_IdStewardess_empty_Then_throws_exception()
        {
            //assign
            CrewDTO crew = new CrewDTO()
            {
                IdPilot = 1
            };

            //assert
            Assert.Throws<Exception>(() => crewService.AddCrew(crew));
        }

        [Test]
        public void AddCrew_When_IdPilot_empty_Then_throws_exception()
        {
            //assign
            CrewDTO crew = new CrewDTO()
            {
                idStewardess = new int[] { 1, 2, 3 }
            };

            //assert
            Assert.Throws<Exception>(() => crewService.AddCrew(crew));
        }

        [Test]
        public void UpdateCrew_When_correct_data_Then_IdPilot_equal_2()
        {
            //assign
            CrewDTO crew = new CrewDTO()
            {
                IdPilot = 2,
                idStewardess = new int[] { 1, 2, 3 }
            };

            //act
            crewService.UpdateCrew(1,crew);

            //assert
            Assert.That(Crews.FirstOrDefault(c=>c.Id==1).IdPilot == 2);
        }

        [Test]
        public void UpdateCrew_When_incorrect_data_Then_throws_execption()
        {
            //assign
            CrewDTO crew = new CrewDTO()
            {
                IdPilot = 2,
            };

            //assert
            Assert.Throws<Exception>(() => crewService.UpdateCrew(1, crew));
        }
    }
}
