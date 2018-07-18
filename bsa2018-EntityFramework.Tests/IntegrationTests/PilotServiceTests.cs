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
using System.Threading.Tasks;

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
        public async Task AddPilot_When_correct_data_Then_check_exists()
        {
            //assing
            pilot.Id = pilotService.AddPilot(pilot).Result.Id;

            //act
            PilotDTO checkPilot = await pilotService.GetPilot(pilot.Id);
            //assert
            Assert.IsNotNull(checkPilot);
        }

        [Test, Order(2)]
        public async Task UpdatePilot()
        {
            //assign
            pilot.Name = "Vladimir";
            //act
            await pilotService.UpdatePilot(pilot.Id,pilot);
            PilotDTO checkPilot = await pilotService.GetPilot(pilot.Id);

            //assert
            Assert.That(checkPilot.Name== "Vladimir");
        }

        [Test, Order(3)]
        public async Task DeletePilot()
        {
            //act
            await pilotService.DeletePilot(pilot.Id);
            PilotDTO result= await pilotService.GetPilot(pilot.Id);

            //assert
           Assert.IsNull(result);
        }

        [OneTimeTearDown]
        public async Task TestDown()
        {
            await pilotService.DeletePilot(pilot.Id);
        }
    }
}
