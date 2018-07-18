using AutoMapper;
using bsa2018_ProjectStructure.BLL.Services;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using bsa2018_ProjectStructure.DataAccess.Model;
using bsa2018_ProjectStructure.DataAccess.Repository;
using bsa2018_ProjectStructure.Shared.DTO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace bsa2018_ProjectStructure.BLL.Tests.IntegrationTests
{
    [TestFixture]
    public class PilotServiceTests
    {
        private readonly PilotService pilotService;
        private PilotDTO pilot;

        public PilotServiceTests()
        {
            IMapper mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            IUnitOfWork unitOfWork = new UnitOfWork(new DataAccess.Model.DataContext());
            pilotService = new PilotService(unitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            pilot = new PilotDTO()
            {
                Name = "Yan",
                Surname = "Gorshkov",
                Birthday = new DateTime(1998, 8, 21),
                Experience = 2
            };
        }

        [Test, Order(1)]
        public void AddPilot_When_correct_data_Then_check_exists()
        {
            //assing
            pilot.Id = pilotService.AddPilot(pilot).Id;

            //act
            PilotDTO checkPilot = pilotService.GetPilot(pilot.Id);
            //assert
            Assert.IsNotNull(checkPilot);
        }

        [Test, Order(2)]
        public void UpdatePilot()
        {
            //assign
            pilot.Name = "Vladimir";
            //act
            pilotService.UpdatePilot(pilot.Id,pilot);
            PilotDTO checkPilot = pilotService.GetPilot(pilot.Id);

            //assert
            Assert.That(checkPilot.Name== "Vladimir");
        }

        [Test, Order(3)]
        public void DeletePilot()
        {
            //act
            pilotService.DeletePilot(pilot.Id);
            PilotDTO result=pilotService.GetPilot(pilot.Id);

            //assert
           Assert.IsNull(result);
        }

        [OneTimeTearDown]
        public void TestDown()
        {
            pilotService.DeletePilot(pilot.Id);
        }
    }
}
