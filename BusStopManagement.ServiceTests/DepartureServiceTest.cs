using AutoFixture;
using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.ServiceContracts;
using BusStopManagement.Core.Services;
using FluentAssertions;
using Moq;

namespace BusStopManagement.ServiceTests
{
    public class DepartureServiceTest
    {
        private readonly IDepartureAdderService _departureAdderService;

        private readonly IDepartureRepository _departureRepository;

        private readonly Mock<IDepartureRepository> _departureRepositoryMock;

        private readonly ITestOutputHelper _testOutputHelper;

        private readonly IFixture _fixture;

        public DepartureServiceTest(ITestOutputHelper testOutputHelper)
        {
            _departureRepositoryMock = new Mock<IDepartureRepository>();
            _departureRepository = _departureRepositoryMock.Object;
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
            _departureAdderService = new DepartureAdderService(_departureRepository);
        }

        #region AddDeparture

        [Fact]
        public async Task AddDeparture_NullDeparture_ToBeArgumentNullException()
        {
            //Arrange
            DepartureAddRequest? departureAddRequest = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _departureAdderService.AddDeparture(departureAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task AddDeparture_DestinationIsNull_ToBeArgumentException()
        {
            //Arrange
            DepartureAddRequest departureAddRequest = _fixture.Build<DepartureAddRequest>().Without(x => x.Destination).Create();

            Departure departure = departureAddRequest.ToDeparture();

            _departureRepositoryMock.Setup(x => x.AddDeparture(It.IsAny<Departure>())).ReturnsAsync(departure);

            //Act
            Func<Task> action = async () =>
            {
                await _departureAdderService.AddDeparture(departureAddRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task AddDeparture_FullDepartureDetails_ToBeSuccessfull()
        {
            //Arrange
            DepartureAddRequest departureAddRequest = _fixture.Build<DepartureAddRequest>().Create();

            Departure departure = departureAddRequest.ToDeparture();

            DepartureResponse departureResponseExpected = departure.ToDepartureResponse();

            _departureRepositoryMock.Setup(x => x.AddDeparture(It.IsAny<Departure>())).ReturnsAsync(departure);

            //Act
            DepartureResponse departureResponseFromAdd = await _departureAdderService.AddDeparture(departureAddRequest);
            departureResponseExpected.DepartureID = departureResponseFromAdd.DepartureID;

            //Assert
            departureResponseFromAdd.DepartureID.Should().NotBe(Guid.Empty);
            departureResponseFromAdd.Should().Be(departureResponseExpected);
        }

        #endregion
    }
}
