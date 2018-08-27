using Persistence;
using PipServices.Commons.Config;
using System.Threading.Tasks;
using Xunit;

namespace Test.Persistence
{
    public class BeaconsMongoDbPersistenceTest
    {
        public BeaconsMongoDbPersistence Persistence { get; set; }
        public BeaconsPersistenceFixture Fixture { get; set; }

        public BeaconsMongoDbPersistenceTest()
        {
            ConfigParams config = ConfigParams.FromTuples(
                "collection", "beacons",
                "connection.uri", "mongodb://localhost:27017/test"
                );

            Persistence = new BeaconsMongoDbPersistence();
            Persistence.Configure(config);
            Persistence.OpenAsync(null).Wait();
            Persistence.ClearAsync(null).Wait();
            Fixture = new BeaconsPersistenceFixture(Persistence);
        }

        [Fact]
        public async Task It_Should_Create_Beacon()
        {
            await Fixture.TestCreateBeacon();
        }

        [Fact]
        public async Task It_Should_Update_Beacon()
        {
            await Fixture.TestUpdateBeacon();
        }

        [Fact]
        public async Task It_Should_Delete_Beacon()
        {
            await Fixture.TestDeleteBeacon();
        }

        [Fact]
        public async Task It_Should_Get_Beacon_By_Id()
        {
            await Fixture.TestGetBeaconById();
        }

        [Fact]
        public async Task It_Should_Get_Beacon_By_Udi()
        {
            await Fixture.TestGetBeaconByUdi();
        }

        [Fact]
        public async Task It_Should_Get_Beacons_By_Filter()
        {
            await Fixture.TestGetBeaconsByFilter();
        }
    }
}
