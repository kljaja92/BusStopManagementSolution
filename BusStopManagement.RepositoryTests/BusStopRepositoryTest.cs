using AutoFixture;
using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Infrastructure.DatabaseContext;
using BusStopManagement.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BusStopManagement.RepositoryTests
{
    public class BusStopRepositoryTest
    {
        private readonly BusStopRepository _busStopRepository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IFixture _fixture;

        public BusStopRepositoryTest(ITestOutputHelper testOutputHelper)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new ApplicationDbContext(options);
            _busStopRepository = new BusStopRepository(context);
            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
        }

        #region AddBusStop

        [Fact]
        public async Task AddBusStop_NullBusStop_ToBeNullReferenceException()
        {
            //Arrange
            BusStop? busStop = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _busStopRepository.AddBusStop(busStop);
            };

            //Assert
            await action.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task AddBusStop_FullBusStop_ToBeSuccessfull()
        {
            //Arrange
            Guid busStopId = Guid.NewGuid();
            BusStop busStop = _fixture.Build<BusStop>().Without(x => x.Departures).With(x => x.BusStopID, busStopId).Create();

            //Act
            _testOutputHelper.WriteLine($"Testing AddBusStop with BusStopID: {busStopId}");
            var result = await _busStopRepository.AddBusStop(busStop);

            //Assert
            _testOutputHelper.WriteLine($"Result BusStopID: {result.BusStopID}");
            result.BusStopID.Should().Be(busStopId);
        }

        #endregion

        #region DeleteBusStop

        [Fact]
        public async Task DeleteNonExistentBusStop_ToBeDbUpdateConcurrencyException()
        {
            //Arrange
            BusStop busStop = _fixture.Build<BusStop>().Without(x => x.Departures).Create();
            await _busStopRepository.AddBusStop(busStop);

            BusStop busStopToDelete = _fixture.Build<BusStop>().Without(x => x.Departures).Create();

            //Act
            Func<Task> action = async () =>
            {
                await _busStopRepository.DeleteBusStop(busStopToDelete);
            };

            //Assert
            await action.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task DeleteBusStop_ToBeSuccessfull()
        {
            //Arrange
            BusStop busStop = _fixture.Build<BusStop>().Without(x => x.Departures).Create();
            await _busStopRepository.AddBusStop(busStop);

            //Act
            var result = await _busStopRepository.DeleteBusStop(busStop);

            //Assert
            result.Should().BeTrue();
        }

        #endregion

        #region GetBusStops

        [Fact]
        public async Task GetBusStops_EmptyList()
        {
            //Arrange/Act
            List<BusStop> busStops = await _busStopRepository.GetBusStops();

            //Assert
            busStops.Should().NotBeNull();
            busStops.Should().BeEmpty();
        }

        [Fact]
        public async Task GetBusStops_ReturnInsertedBusStops_ToBeSuccessfull()
        {
            //Arrange
            BusStop busStop1 = _fixture.Build<BusStop>().Without(x => x.Departures).Create();
            BusStop busStop2 = _fixture.Build<BusStop>().Without(x => x.Departures).Create();

            await _busStopRepository.AddBusStop(busStop1);
            await _busStopRepository.AddBusStop(busStop2);

            //Act
            List<BusStop> busStops = await _busStopRepository.GetBusStops();

            //Assert
            busStops.Should().NotBeNull();
            busStops.Should().Contain(x => x.BusStopID == busStop1.BusStopID);
            busStops.Should().Contain(x => x.BusStopID == busStop2.BusStopID);
            busStops.Count().Should().BeGreaterThanOrEqualTo(2);
        }

        #endregion

        #region UpdateBusStop

        [Fact]
        public async Task UpdateBusStop_NullBusStop_ToBeNullReferenceException()
        {
            //Arrange
            BusStop? busStop = null!;

            //Act
            Func<Task> action = async () =>
            {
                await _busStopRepository.AddBusStop(busStop);
            };

            //Assert
            await action.Should().ThrowAsync<NullReferenceException>();
        }

        [Fact]
        public async Task UpdateBusStop_BusStopNotPresentInDatabase_ToBeDbUpdateConcurrencyException()
        {
            //Arrange
            BusStop busStop = _fixture.Build<BusStop>().With(x => x.BusStopName, "Original").With(x => x.BusStopAddress, "AddressOriginal").Without(x => x.Departures).Create();
            await _busStopRepository.AddBusStop(busStop);

            BusStop busStopUpdated = _fixture.Build<BusStop>().With(x => x.BusStopName, "Updated").With(x => x.BusStopAddress, "AddressUpdated").Without(x => x.Departures).Create();

            //Act
            Func<Task> action = async () =>
            {
                await _busStopRepository.UpdateBusStop(busStopUpdated);
            };

            //Assert
            await action.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task UpdateBusStop_FullBusStop_ToBeSuccessfull()
        {
            //Arrange
            BusStop busStop = _fixture.Build<BusStop>().With(x => x.BusStopName, "Original").With(x => x.BusStopAddress, "AddressOriginal").Without(x => x.Departures).Create();
            await _busStopRepository.AddBusStop(busStop);

            busStop.BusStopName = "Updated";
            busStop.BusStopAddress = "AddressUpdated";

            //Act
            var result = await _busStopRepository.UpdateBusStop(busStop);
            var busStops = await _busStopRepository.GetBusStops();

            //Assert
            result.BusStopName.Should().Be("Updated");
            result.BusStopAddress.Should().Be("AddressUpdated");
            busStops.Should().Contain(x => x.BusStopID == busStop.BusStopID && x.BusStopName == "Updated" && x.BusStopAddress == "AddressUpdated");
        }

        #endregion

        #region GetBusStopByBusStopId

        [Fact]
        public async Task GetBusStopById_NullBusStop_ToBeNull()
        {
            //Arrange
            Guid busStopId = Guid.NewGuid();

            //Act
            BusStop? busStop = await _busStopRepository.GetBusStopByBusStopId(busStopId);

            //Assert
            busStop.Should().BeNull();
        }

        [Fact]
        public async Task GetBusStopById_BusStopExists_ToBeSuccessful()
        {
            //Arrange
            Guid testGuid = Guid.NewGuid();
            _testOutputHelper.WriteLine($"Guid for testing: {testGuid}");

            BusStop busStop = _fixture.Build<BusStop>().With(x => x.BusStopID, testGuid).Without(x => x.Departures).Create();

            await _busStopRepository.AddBusStop(busStop);

            //Act
            BusStop? busStopFromDb = await _busStopRepository.GetBusStopByBusStopId(testGuid);

            //Assert
            busStopFromDb.Should().NotBeNull();
            busStopFromDb.BusStopID.Should().Be(testGuid);
            _testOutputHelper.WriteLine($"Guid from database: {busStopFromDb.BusStopID}");
        }

        #endregion

        #region GetBusStopByBusStopName

        [Fact]
        public async Task GetBusStopByBusStopName_NullBusStopName_ToBeNull()
        {
            //Arrange
            string busStopName = "Test";

            //Act
            BusStop? busStopFromDb = await _busStopRepository.GetBusStopByBusStopName(busStopName);

            //Assert
            busStopFromDb.Should().BeNull();
        }

        [Fact]
        public async Task GetBusStopByBusStopName_BusStopNameExists_ToBeSuccessful()
        {
            //Arrange
            string busStopName = "Test";
            _testOutputHelper.WriteLine($"Bus stop name for testing: {busStopName}");

            BusStop busStop = _fixture.Build<BusStop>().With(x => x.BusStopName, busStopName).Without(x => x.Departures).Create();

            await _busStopRepository.AddBusStop(busStop);

            //Act
            BusStop? busStopFromDb = await _busStopRepository.GetBusStopByBusStopName(busStopName);

            //Assert
            busStopFromDb.Should().NotBeNull();
            busStopFromDb.BusStopName.Should().Be(busStopName);
            _testOutputHelper.WriteLine($"Bus stop name from database: {busStopFromDb.BusStopName}");
        }

        #endregion

    }
}
