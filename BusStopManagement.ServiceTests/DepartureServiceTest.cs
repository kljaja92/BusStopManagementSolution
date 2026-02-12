using AutoFixture;
using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.ServiceContracts;
using BusStopManagement.Core.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BusStopManagement.ServiceTests
{
    public class DepartureServiceTest
    {
        private readonly IDepartureAdderService _departureAdderService;
        private readonly IDepartureDeleterService _departureDeleterService;
        private readonly IDepartureGetterService _departureGetterService;
        private readonly IDepartureUpdaterService _departureUpdaterService;

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
            _departureDeleterService = new DepartureDeleterService(_departureRepository);
            _departureGetterService = new DepartureGetterService(_departureRepository);
            _departureUpdaterService = new DepartureUpdaterService(_departureRepository);
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
        public async Task AddDeparture_FullDepartureDetails_ToBeSuccessful()
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

        #region DeleteDeparture

        [Fact]
        public async Task DeleteDeparture_NullDeparture_ToBeArgumentNullException()
        {
            //Arrange
            Departure? departure = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _departureDeleterService.DeleteDeparture(departure);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteDeparture_DepartureExists_ToBeSuccessful()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).Create();

            _departureRepositoryMock.Setup(x => x.DeleteDeparture(It.IsAny<Departure>())).ReturnsAsync(true);

            //Act
            bool isDeleted = await _departureDeleterService.DeleteDeparture(departure);

            //Assert
            isDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteDeparture_DepartureNotPresentInDatabase_ToBeDbUpdateConcurrencyException()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).Create();

            _departureRepositoryMock.Setup(x => x.DeleteDeparture(It.IsAny<Departure>())).ThrowsAsync(new DbUpdateConcurrencyException("Attempted to delete entity that doesn't exist in database"));

            //Act
            Func<Task> action = async () =>
            {
                await _departureDeleterService.DeleteDeparture(departure);
            };

            //Assert
            await action.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        #endregion

        #region GetDepartures

        [Fact]
        public async Task GetDepartures_NoDeparturesInDatabase_ToBeEmptyList()
        {
            //Arrange
            _departureRepositoryMock.Setup(x => x.GetDepartures()).ReturnsAsync(new List<Departure>());

            //Act
            List<DepartureResponse> departureResponsesFromGet = await _departureGetterService.GetDepartures();

            //Assert
            Assert.Empty(departureResponsesFromGet);
            departureResponsesFromGet.Should().BeEmpty();
        }

        [Fact]
        public async Task GetDepartures_DeparturesExist_ToBeSuccessful()
        {
            //Arrange
            List<Departure> departures = new List<Departure>()
            {
                _fixture.Build<Departure>().Without(x => x.BusStop).Create(),
                _fixture.Build<Departure>().Without(x => x.BusStop).Create(),
                _fixture.Build<Departure>().Without(x => x.BusStop).Create()
            };

            List<DepartureResponse> departureResponsesListFromExpected = departures.Select(x => x.ToDepartureResponse()).ToList();

            _testOutputHelper.WriteLine("Expected: ");
            foreach (DepartureResponse departureResponseFromExpected in departureResponsesListFromExpected)
                _testOutputHelper.WriteLine(departureResponseFromExpected.ToString());

            _departureRepositoryMock.Setup(x => x.GetDepartures()).ReturnsAsync(departures);

            //Act
            List<DepartureResponse> departureResponsesFromGet = await _departureGetterService.GetDepartures();

            _testOutputHelper.WriteLine("Actual: ");
            foreach (DepartureResponse departureResponseFromGet in departureResponsesFromGet)
                _testOutputHelper.WriteLine(departureResponseFromGet.ToString());

            //Assert
            departureResponsesFromGet.Should().BeEquivalentTo(departureResponsesListFromExpected);
        }

        #endregion

        #region GetDepartureByDepartureID

        [Fact]
        public async Task GetDepartureByDepartureID_NullDepartureID_ToBeNull()
        {
            //Arrange
            Guid? departureID = null;

            //Act
            DepartureResponse? departureResponseFromGet = await _departureGetterService.GetDepartureByDepartureID(departureID);

            //Assert
            departureResponseFromGet.Should().BeNull();
        }

        [Fact]
        public async Task GetDepartureByDepartureID_WithDepartureID_ToBeSuccessful()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).Create();
            
            DepartureResponse departureResponseExpected = departure.ToDepartureResponse();

            _departureRepositoryMock.Setup(x => x.GetDepartureByDepartureId(It.IsAny<Guid>())).ReturnsAsync(departure);

            //Act
            DepartureResponse? departureResponseFromGet = await _departureGetterService.GetDepartureByDepartureID(departure.DepartureID);

            //Assert
            departureResponseFromGet.Should().Be(departureResponseExpected);
        }

        #endregion

        #region UpdateDeparture

        [Fact]
        public async Task UpdateDeparture_NullDeparture_ToBeArgumentNullException()
        {
            //Arrange
            DepartureUpdateRequest? departureUpdateRequest = null;

            //Act
            Func<Task> action = async () =>
            {
                await _departureUpdaterService.UpdateDeparture(departureUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateDeparture_InvalidDepartureID_ToBeArgumentException()
        {
            //Arrange
            DepartureUpdateRequest departureUpdateRequest = _fixture.Build<DepartureUpdateRequest>().Create();

            //Act
            Func<Task> action = async () =>
            {
                await _departureUpdaterService.UpdateDeparture(departureUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateDeparture_DestinationIsNull_ToBeArgumentNullException()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.Destination).Without(x => x.BusStop).Create();

            DepartureResponse departureResponseFromAdd = departure.ToDepartureResponse();

            DepartureUpdateRequest departureUpdateRequest = departureResponseFromAdd.ToDepartureUpdateRequest();
            departureUpdateRequest.Destination = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _departureUpdaterService.UpdateDeparture(departureUpdateRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task UpdateDeparture_FullDeparture_ToBeSuccessful()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).Create();

            DepartureResponse departureResponseExpected = departure.ToDepartureResponse();

            DepartureUpdateRequest departureUpdateRequest = departureResponseExpected.ToDepartureUpdateRequest();

            _departureRepositoryMock.Setup(x => x.GetDepartureByDepartureId(It.IsAny<Guid>())).ReturnsAsync(departure);
            _departureRepositoryMock.Setup(x => x.UpdateDeparture(It.IsAny<Departure>())).ReturnsAsync(departure);

            //Act
            DepartureResponse departureResponseFromUpdate = await _departureUpdaterService.UpdateDeparture(departureUpdateRequest);

            //Assert
            departureResponseFromUpdate.Should().Be(departureResponseExpected);
        }

        #endregion
    }
}
