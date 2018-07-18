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
    public class TicketServiceTests
    {
        private List<Ticket> Tickets { get; set; }
        private readonly IUnitOfWork fakeUnitOfWork;
        private readonly IRepository<Ticket> fakeTicketRepository;
        private readonly ITicketService ticketService;
        private readonly IMapper mapper;

        public TicketServiceTests()
        {
            mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            fakeTicketRepository = A.Fake<IRepository<Ticket>>();
            fakeUnitOfWork = A.Fake<IUnitOfWork>();
            Tickets = new List<Ticket>();
            ticketService = new TicketService(fakeUnitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            A.CallTo(() => fakeTicketRepository.Create(A<Ticket>._)).Invokes((Ticket a) => { a.Id = Tickets.Count + 1; Tickets.Add(a); });
            A.CallTo(() => fakeTicketRepository.Update(A<int>._, A<Ticket>._)).Invokes((int id, Ticket c) => { Tickets.FirstOrDefault(air => air.Id == 1).Cost = c.Cost; });
            A.CallTo(() => fakeUnitOfWork.Tickets).Returns(fakeTicketRepository);
        }

        [Test]
        public void AddTicket_When_correct_data_Then_count_equal_1()
        {
            //assign
            TicketDTO ticket = new TicketDTO()
            {
                Cost=100,
                FlightNumber=1
            };

            //acts
            ticketService.AddTicket(ticket);

            //assert
            Assert.That(Tickets.Count == 1);
        }

        [Test]
        public void AddTicket_When_negative_FlightNumber_Then_throws_exception()
        {
            //assign
            TicketDTO ticket = new TicketDTO()
            {
                Cost = 100,
                FlightNumber = -21
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => ticketService.AddTicket(ticket));
        }


        [Test]
        public void AddTicket_When_negative_Cost_Then_throws_exception()
        {
            //assign
            TicketDTO ticket = new TicketDTO()
            {
                Cost = -100,
                FlightNumber = 100
            };

            //assert
            Assert.ThrowsAsync<Exception>(() => ticketService.AddTicket(ticket));
        }

        [Test]
        public void UpdateTicket_When_correct_data_Then_Cost_equal_200()
        {
            //assign
            TicketDTO ticket = new TicketDTO()
            {
                Cost = 200,
                FlightNumber = 1
            };

            //act
            ticketService.UpdateTicket(1, ticket);

            //assert
            Assert.That(Tickets.FirstOrDefault(c => c.Id == 1).Cost == 200);
        }
    }
}
