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
    public class TicketServiceTests
    {
        private readonly TicketService ticketService;
        private TicketDTO ticket;

        public TicketServiceTests()
        {
            IMapper mapper = new Shared.DTO.MapperConfiguration().Configure().CreateMapper();
            IUnitOfWork unitOfWork = new UnitOfWork(new DataAccess.Model.DataContext());
            ticketService = new TicketService(unitOfWork, mapper);
        }

        [OneTimeSetUp]
        public void TestSetup()
        {
            ticket = new TicketDTO()
            {
                Cost=100,
                FlightNumber=2
            };
        }

        [Test, Order(1)]
        public void AddTicket_When_correct_data_Then_check_exists()
        {
            //assing
            ticket.Id = ticketService.AddTicket(ticket).Id;

            //act
            TicketDTO checkTicket = ticketService.GetTicket(ticket.Id);
            //assert
            Assert.IsNotNull(checkTicket);
        }

        [Test, Order(2)]
        public void UpdateTicket()
        {
            //assign
            ticket.Cost= 150;
            //act
            ticketService.UpdateTicket(ticket.Id, ticket);
            TicketDTO checkTicket = ticketService.GetTicket(ticket.Id);

            //assert
            Assert.That(checkTicket.Cost == 150);
        }

        [Test, Order(3)]
        public void DeleteTicket()
        {
            //act
            ticketService.DeleteTicket(ticket.Id);
            TicketDTO result = ticketService.GetTicket(ticket.Id);

            //assert
            Assert.IsNull(result);
        }

        [OneTimeTearDown]
        public void TestDown()
        {
            ticketService.DeleteTicket(ticket.Id);
        }
    }
}
