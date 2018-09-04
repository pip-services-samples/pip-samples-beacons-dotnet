using Service.Persistence;
using System.Threading.Tasks;
using Xunit;

namespace Test.Persistence
{
    public class MemoryBeaconsPersistenceTest
    {
        public BeaconsMemoryPersistence Persistence { get; set; }
        public BeaconsPersistenceFixture Fixture { get; set; }

        public MemoryBeaconsPersistenceTest()
        {
            Persistence = new BeaconsMemoryPersistence();
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
