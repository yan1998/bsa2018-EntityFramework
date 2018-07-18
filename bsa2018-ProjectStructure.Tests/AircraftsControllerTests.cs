using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FakeItEasy;
using bsa2018_ProjectStructure.Shared.DTO;
using bsa2018_ProjectStructure.Controllers;
using AutoMapper;
using bsa2018_ProjectStructure.DataAccess.Model;
using bsa2018_ProjectStructure.DataAccess.Interfaces;
using bsa2018_ProjectStructure.BLL.Services;
using bsa2018_ProjectStructure.BLL.Interfaces;

namespace bsa2018_ProjectStructure.Tests
{
    [TestFixture]
    public class AircraftsControllerTests
    {
        private readonly AircraftsController aircraftsController;
        private readonly IMapper mapper;
        private readonly IRepository<Aircraft> fakeAircraftRepository;
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IAircraftService fakeAircraftService;
        private List<AircraftDTO> Aircrafts { get; set; }
       
        public AircraftsControllerTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeAircraftRepository = A.Fake<IRepository<Aircraft>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Aircrafts = new List<AircraftDTO>();
            fakeAircraftService = A.Fake<IAircraftService>();
            aircraftsController = new AircraftsController(fakeAircraftService);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeAircraftService.AddAircraft(A<AircraftDTO>._)).Invokes((AircraftDTO a)=> { a.Id = Aircrafts.Count+1; Aircrafts.Add(a); });
            A.CallTo(() => fakeAircraftService.DeleteAircraft(A<int>._)).Invokes((int id) => Aircrafts.RemoveAll(a=>a.Id==id));
            A.CallTo(() => fakeAircraftService.UpdateAircraft(A<int>._,A<AircraftDTO>._)).Returns(Aircrafts.FirstOrDefault());
            A.CallTo(() => fakeAircraftService.GetAircraft(A<int>._)).Returns(Aircrafts.FirstOrDefault());
            A.CallTo(() => fakeAircraftService.GetAllAircrafts()).Returns(Aircrafts);
            A.CallTo(() => fakeAircraftRepository.Create(A<Aircraft>._));
            A.CallTo(() => fakeAircraftRepository.Update(A<int>._, A<Aircraft>._));
            A.CallTo(() => fakeAircraftRepository.Delete(A<int>._));
            A.CallTo(() => fakeAircraftRepository.GetById(A<int>._));
            A.CallTo(() => fakeAircraftRepository.GetAll());
            A.CallTo(() => fakeUnitOfWork.Aircrafts).Returns(fakeAircraftRepository);
        }

        [Test,Order(1)]
        public void Post()
        {
            AircraftDTO aircraft = new AircraftDTO()
            {
                IdAircraftType=1,
                LifeSpan=new TimeSpan(),
                Name="fsdfsdf",
                ReleaseDate=new DateTime()
            };
            //act
            aircraftsController.Post(aircraft);

            //assert
            Assert.That(Aircrafts.Count==1);
        }

        [Test, Order(2)]
        public void Get_Without_Arguments()
        {
            //act
            string result = aircraftsController.Get().ToString();

            //assert
            Assert.That(result != "null");
        }

        [Test, Order(3)]
        public void Get()
        {
            //act
            string result=aircraftsController.Get(1).ToString();

            //assert
            Assert.That(result != "null");
        }

        [Test, Order(4)]
        public void Put()
        {
            //act
            string result=aircraftsController.Put(1,new AircraftDTO()).ToString();

            //assert
            Assert.That(result != "null");
        }


        [Test, Order(5)]
        public void Delete()
        {
            //assert
            Assert.DoesNotThrow(() => aircraftsController.Delete(1));
        }
    }
}
