using AutoFixture;
using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Infrastructure.DatabaseContext;
using BusStopManagement.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BusStopManagement.RepositoryTests
{
    public class DepartureRepositoryTest
    {
        private readonly DepartureRepository _departureRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public DepartureRepositoryTest(ITestOutputHelper testOutputHelper)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new ApplicationDbContext(options);
            _departureRepository = new DepartureRepository(context);
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
        }

        #region AddDeparture

        [Fact]
        public async Task AddDeparture_NullDeparture_ToBeNullReferenceException()
        {
            //Arrange
            Departure? departure = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _departureRepository.AddDeparture(departure);
            };

            //Assert
            await action.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task AddDeparture_FullDeparture_ToBeSuccessfull()
        {
            //Arrange
            Guid busStopId = Guid.NewGuid();
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).With(x => x.BusStopID, busStopId).Create();

            //Act
            _testOutputHelper.WriteLine($"Testing AddDeparture with BusStopID: {busStopId}");
            var result = await _departureRepository.AddDeparture(departure);

            //Assert
            _testOutputHelper.WriteLine($"Result DepartureID: {result.DepartureID}");
            result.BusStopID.Should().Be(busStopId);
        }

        #endregion

        #region DeleteDeparture

        [Fact]
        public async Task DeleteNonExistentDeparture_ToBeDbUpdateConcurrencyException()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).Create();
            await _departureRepository.AddDeparture(departure);

            Departure departureToDelete = _fixture.Build<Departure>().Without(x => x.BusStop).Create();

            //Act
            Func<Task> action = async () =>
            {
                await _departureRepository.DeleteDeparture(departureToDelete);
            };

            //Assert
            await action.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task DeleteDeparture_ToBeSuccessfull()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().Without(x => x.BusStop).Create();
            await _departureRepository.AddDeparture(departure);

            //Act
            var result = await _departureRepository.DeleteDeparture(departure);

            //Assert
            result.Should().BeTrue();
        }

        #endregion

        #region GetDepartures

        [Fact]
        public async Task GetDepartures_EmptyList()
        {
            //Arrange/Act
            List<Departure> departures = await _departureRepository.GetDepartures();

            //Assert
            departures.Should().NotBeNull();
            departures.Should().BeEmpty();
        }

        [Fact]
        public async Task GetDepartures_ReturnInsertedDepartures_ToBeSuccessfull()
        {
            //Arrange
            Departure departure1 = _fixture.Build<Departure>().Without(x => x.BusStop).Create();
            Departure departure2 = _fixture.Build<Departure>().Without(x => x.BusStop).Create();

            await _departureRepository.AddDeparture(departure1);
            await _departureRepository.AddDeparture(departure2);

            //Act
            List<Departure> departures = await _departureRepository.GetDepartures();

            //Assert
            departures.Should().NotBeNull();
            departures.Should().Contain(x => x.DepartureID == departure1.DepartureID);
            departures.Should().Contain(x => x.DepartureID == departure2.DepartureID);
            departures.Count().Should().BeGreaterThanOrEqualTo(2);
        }

        #endregion

        #region UpdateDeparture

        [Fact]
        public async Task UpdateDeparture_NullDeparture_ToBeNullReferenceException()
        {
            //Arrange
            Departure? departure = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _departureRepository.AddDeparture(departure);
            };

            //Assert
            await action.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task UpdateDeparture_DepartureNotPresentInDatabase_ToBeDbUpdateConcurrencyException()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().With(x => x.Destination, "Original").With(x => x.NumberOfSeats, (byte)40).Without(x => x.BusStop).Create();
            await _departureRepository.AddDeparture(departure);

            Departure departureUpdated = _fixture.Build<Departure>().With(x => x.Destination, "Updated").With(x => x.NumberOfSeats, (byte)40).Without(x => x.BusStop).Create();

            //Act
            Func<Task> action = async () =>
            {
                await _departureRepository.UpdateDeparture(departureUpdated);
            };

            //Assert
            await action.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task UpdateDeparture_FullDeparture_ToBeSuccessfull()
        {
            //Arrange
            Departure departure = _fixture.Build<Departure>().With(x => x.Destination, "Original").With(x => x.NumberOfSeats, (byte)40).Without(x => x.BusStop).Create();
            await _departureRepository.AddDeparture(departure);

            departure.Destination = "Updated";
            departure.NumberOfSeats = 55;
            departure.DateAndTimeOfDeparture = DateTime.UtcNow.AddHours(2);

            //Act
            var result = await _departureRepository.UpdateDeparture(departure);
            var departures = await _departureRepository.GetDepartures();

            //Assert
            result.Destination.Should().Be("Updated");
            result.NumberOfSeats.Should().Be(55);
            departures.Should().Contain(x => x.DepartureID == departure.DepartureID && x.Destination == "Updated" && x.NumberOfSeats == 55);
        }

        #endregion

        #region GetDepartureByDepartureId

        [Fact]
        public async Task GetDepartureByDepartureId_NullDepartureId_ToBeNull()
        {
            //Arrange
            Guid departureId = Guid.NewGuid();

            //Act
            Departure? departureFromDb = await _departureRepository.GetDepartureByDepartureId(departureId);

            //Assert
            departureFromDb.Should().BeNull();
        }

        [Fact]
        public async Task GetDepartureByDepartureId_DepartureExists_ToBeSuccessful()
        {
            //Arrange
            Guid testGuid = Guid.NewGuid();
            _testOutputHelper.WriteLine($"Guid for testing: {testGuid}");

            Departure departure = _fixture.Build<Departure>().With(x => x.DepartureID, testGuid).Without(x => x.BusStop).Create();

            await _departureRepository.AddDeparture(departure);

            //Act
            Departure? departureFromDb = await _departureRepository.GetDepartureByDepartureId(testGuid);

            //Assert
            departureFromDb.Should().NotBeNull();
            departureFromDb.DepartureID.Should().Be(testGuid);
            _testOutputHelper.WriteLine($"Guid from database: {departureFromDb.DepartureID}");
        }

        #endregion
    }
}
