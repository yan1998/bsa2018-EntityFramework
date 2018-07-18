using AutoMapper;
using bsa2018_ProjectStructure.BLL.Services;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using bsa2018_ProjectStructure.DataAccess.Repository;
using bsa2018_ProjectStructure.Shared.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace bsa2018_ProjectStructure.BLL.Tests.IntegrationTests
{
    [TestFixture]
    public class CrewServiceTests
    {
        private readonly CrewService crewService;
        private CrewDTO crew;

        public CrewServiceTests()
        {
            IMapper mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            IUnitOfWork unitOfWork = new UnitOfWork(new DataAccess.Model.DataContext());
            crewService = new CrewService(unitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            crew = new CrewDTO()
            {
                IdPilot=1,
                idStewardess=new int[] { 1,2}
            };
        }

        [Test, Order(1)]
        public void AddCrew_When_correct_data_Then_check_exists()
        {
            //assing
            crew.Id = crewService.AddCrew(crew).Id;

            //act
            CrewDTO checkCrew = crewService.GetCrew(crew.Id);
            //assert
            Assert.IsNotNull(checkCrew);
        }

        [Test, Order(2)]
        public void UpdateCrew()
        {
            //assign
            crew.IdPilot = 2;
            //act
            crewService.UpdateCrew(crew.Id, crew);
            CrewDTO checkCrew = crewService.GetCrew(crew.Id);

            //assert
            Assert.That(checkCrew.IdPilot == 2);
        }

        [Test, Order(3)]
        public void DeleteCrew()
        {
            //act
            crewService.DeleteCrew(crew.Id);
            CrewDTO result = crewService.GetCrew(crew.Id);

            //assert
            Assert.IsNull(result);
        }

        [OneTimeTearDown]
        public void TestDown()
        {
            crewService.DeleteCrew(crew.Id);
        }
    }
}
