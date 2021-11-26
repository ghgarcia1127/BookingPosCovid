using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PostCovidBooking.Core.Dto;
using PostCovidBooking.Core.Interfaces;
using PostCovidBooking.Core.Services;
using PostCovidBooking.Data.Interfaces;
using PostCovidBooking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostCovidBooking.Test.Core
{
    [TestClass]
    public class ReservationServiceTest
    {
        private IReservationRepository repository;
        private IReservationService service;
        private IMapper mapper;

        [TestInitialize]
        public void Initialize()
        {
            repository = Substitute.For<IReservationRepository>();
            mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap()));

            service = new ReservationService(repository, mapper);
        }

        [TestMethod]
        public async Task CreateReservationReturnsFalseOnNoAvailabilityTest()
        {
            //Arrange
            repository.GetAllAsync(Arg.Any<Expression<Func<Reservation, bool>>>())
                      .Returns(new List<Reservation> { new Reservation { InitialDate = DateTime.Now.AddDays(1), FinalDate = DateTime.Now.AddDays(4) } });
            //Act
            var result = await service.CreateReservation(new ReservationDTO { InitialDate = DateTime.Now.AddDays(1), FinalDate = DateTime.Now.AddDays(4) }).ConfigureAwait(false);
            //Assert
            Assert.IsFalse(result);
        }


        [TestMethod]
        public async Task CreateReservationReturnsTrueTest()
        {
            //Arrange
            repository.GetAllAsync(Arg.Any<Expression<Func<Reservation, bool>>>())
                      .Returns(new List<Reservation>());
            //Act
            var result = await service.CreateReservation(new ReservationDTO { InitialDate = DateTime.Now.AddDays(1), FinalDate = DateTime.Now.AddDays(4) }).ConfigureAwait(false);
            //Assert
            Assert.IsTrue(result);
        }
    }
}
