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
    public class StewardessServiceTests
    {
        private List<Stewardess> Stewardess { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Stewardess> fakeStewardessRepository;
        private readonly IStewardessService stewardessService;
        private readonly IMapper mapper;

        public StewardessServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeStewardessRepository = A.Fake<IRepository<Stewardess>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Stewardess = new List<Stewardess>();
            stewardessService = new StewardessService(fakeUnitOfWork, mapper);
        }

        [SetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeStewardessRepository.Create(A<Stewardess>._)).Invokes((Stewardess a) => { a.Id = Stewardess.Count + 1; Stewardess.Add(a); });
            A.CallTo(() => fakeStewardessRepository.Update(A<int>._, A<Stewardess>._)).Invokes((int id, Stewardess c) => { Stewardess.FirstOrDefault(air => air.Id == 1).Name = c.Name; });
            A.CallTo(() => fakeUnitOfWork.Stewardess).Returns(fakeStewardessRepository);
        }

        [Test]
        public void AddStewardess_When_correct_data_Then_count_equal_1()
        {
            //assign
            StewardessDTO stewardess = new StewardessDTO()
            {
                Name = "Anna",
                Surname = "Gorshkova",
                Birthday = new DateTime(1998, 8, 21)
            };

            //acts
            stewardessService.AddStewardess(stewardess);

            //assert
            Assert.That(Stewardess.Count == 1);
        }

        [Test]
        public void AddStewardess_When_empty_name_Then_throws_exception()
        {
            //assign
            StewardessDTO stewardess = new StewardessDTO()
            {
                Surname = "Gorshkova",
                Birthday = new DateTime(1998, 8, 21),
            };

            //assert
            Assert.Throws<Exception>(() => stewardessService.AddStewardess(stewardess));
        }

        [Test]
        public void AddStewardess_When_empty_surname_Then_throws_exception()
        {
            //assign
            StewardessDTO stewardess = new StewardessDTO()
            {
                Name = "Anna",
                Birthday = new DateTime(1998, 8, 21),
            };

            //assert
            Assert.Throws<Exception>(() => stewardessService.AddStewardess(stewardess));
        }

        [Test]
        public void UpdateStewardess_When_correct_data_Then_Name_equal_Test()
        {
            //assign
            StewardessDTO stewardess = new StewardessDTO()
            {
                Name = "Test",
                Surname = "Gorshkova",
                Birthday = new DateTime(1998, 8, 21)
            };

            //act
            stewardessService.UpdateStewardess(1, stewardess);

            //assert
            Assert.That(Stewardess.FirstOrDefault(c => c.Id == 1).Name == "Test");
        }
    }
}
