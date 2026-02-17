using AutoFixture;
using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.Exceptions;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.ServiceContracts;
using BusStopManagement.Core.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusStopManagement.ServiceTests
{
    public class BusStopServiceTest
    {
        private readonly IBusStopAdderService _busStopAdderService;

        private readonly IBusStopRepository _busStopRepository;

        private readonly Mock<IBusStopRepository> _busStopRepositoryMock;

        private readonly ITestOutputHelper _testOutputHelper;

        private readonly IFixture _fixture;

        public BusStopServiceTest(ITestOutputHelper testOutputHelper)
        {
            _busStopRepositoryMock = new Mock<IBusStopRepository>();
            _busStopRepository = _busStopRepositoryMock.Object;

            _testOutputHelper = testOutputHelper;

            _fixture = new Fixture();

            _busStopAdderService = new BusStopAdderService(_busStopRepository);
        }

        #region AddBusStop

        [Fact]
        public async Task AddBusStop_NullBusStop_ToBeArgumentNullException()
        {
            //Arrange
            BusStopAddRequest? busStopAddRequest = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _busStopAdderService.AddBusStop(busStopAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddBusStop_BusStopNameIsNull_ToBeArgumentException()
        {
            //Arrange
            BusStopAddRequest busStopAddRequest = _fixture.Build<BusStopAddRequest>().Without(x => x.BusStopName).Create();

            BusStop busStop = busStopAddRequest.ToBusStop();

            _busStopRepositoryMock.Setup(x => x.AddBusStop(It.IsAny<BusStop>())).ReturnsAsync(busStop);

            //Act
            Func<Task> action = async () =>
            {
                await _busStopAdderService.AddBusStop(busStopAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task AddBusStop_DuplicateBusStop_ToBeDuplicateBusStopNameException()
        {
            //Arrange
            BusStopAddRequest? request1 = _fixture.Build<BusStopAddRequest>().With(x => x.BusStopName, "Test").Create();
            BusStopAddRequest? request2 = _fixture.Build<BusStopAddRequest>().With(x => x.BusStopName, "Test").Create();

            BusStop busStop1 = request1.ToBusStop();
            BusStop busStop2 = request2.ToBusStop();

            _busStopRepositoryMock.Setup(x => x.AddBusStop(It.IsAny<BusStop>())).ReturnsAsync(busStop1);
            _busStopRepositoryMock.Setup(x => x.GetBusStopByBusStopName(It.IsAny<string>())).ReturnsAsync(null as BusStop);

            BusStopResponse busStopResponse = await _busStopAdderService.AddBusStop(request1);

            //Act
            Func<Task> action = async () =>
            {
                _busStopRepositoryMock.Setup(x => x.AddBusStop(It.IsAny<BusStop>())).ReturnsAsync(busStop1);
                _busStopRepositoryMock.Setup(x => x.GetBusStopByBusStopName(It.IsAny<string>())).ReturnsAsync(busStop1);

                await _busStopAdderService.AddBusStop(request2);
            };

            //Assert
            await action.Should().ThrowAsync<DuplicateBusStopNameException>();
        }

        [Fact]
        public async Task AddBusStop_FullBusStop_ToBeSuccessful()
        {
            //Arrange
            BusStopAddRequest? busStopAddRequest = _fixture.Create<BusStopAddRequest>();

            BusStop busStop = busStopAddRequest.ToBusStop();
            BusStopResponse busStopResponseExpected = busStop.ToBusStopResponse();
            

            _busStopRepositoryMock.Setup(x => x.AddBusStop(It.IsAny<BusStop>())).ReturnsAsync(busStop);
            _busStopRepositoryMock.Setup(x => x.GetBusStopByBusStopName(It.IsAny<string>())).ReturnsAsync(null as BusStop);

            //Act
            BusStopResponse busStopResponse = await _busStopAdderService.AddBusStop(busStopAddRequest);
            busStop.BusStopID = busStopResponse.BusStopID;
            busStopResponseExpected.BusStopID = busStopResponse.BusStopID;

            //Assert
            busStopResponse.BusStopID.Should().NotBe(Guid.Empty);
            busStopResponse.Should().BeEquivalentTo(busStopResponseExpected);
            _testOutputHelper.WriteLine($"Bus stop id from db: {busStopResponse.BusStopID}");
        }

        #endregion

    }
}
