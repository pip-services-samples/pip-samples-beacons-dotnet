using Persistence;
using PipServices.Commons.Data;
using System;
using System.Threading.Tasks;
using Test.Interfaces.Data.Version1;
using Xunit;

namespace Test.Persistence
{
    public class BeaconsPersistenceFixture
    {
        private IBeaconsPersistence _persistence;

        public BeaconsPersistenceFixture(IBeaconsPersistence persistence)
        {
            _persistence = persistence;
        }

        public async Task TestCreateBeacon()
        {

            var beacon = TestModel.CreateBeacon();

            var result = await _persistence.CreateAsync(null, beacon);

            TestModel.AssertEqual(beacon, result);
        }

        public async Task TestUpdateBeacon()
        {

            var beacon = await _persistence.CreateAsync(null, TestModel.CreateBeacon());

            beacon.Label = "Update Label";
            beacon.Type = "Update Type";

            var result = await _persistence.UpdateAsync(null, beacon);

            TestModel.AssertEqual(beacon, result);
        }

        public async Task TestDeleteBeacon()
        {
            var beacon = await _persistence.CreateAsync(null, TestModel.CreateBeacon());

            var deletedBeacon = await _persistence.DeleteByIdAsync(null, beacon.Id);
            var result = await _persistence.GetOneByIdAsync(null, beacon.Id);

            TestModel.AssertEqual(beacon, deletedBeacon);
            Assert.Null(result);
        }

        public async Task TestGetBeaconById()
        {
            var beacon = await _persistence.CreateAsync(null, TestModel.CreateBeacon());

            var result = await _persistence.GetOneByIdAsync(null, beacon.Id);

            TestModel.AssertEqual(beacon, result);
        }

        public async Task TestGetBeaconByUdi()
        {
            var beacon = await _persistence.CreateAsync(null, TestModel.CreateBeacon());

            var result = await _persistence.GetOneByIdAsync(null, beacon.Udi);

            TestModel.AssertEqual(beacon, result);
        }

        public async Task TestGetBeaconsByFilter()
        {
            var beacon1 = await _persistence.CreateAsync(null, TestModel.CreateBeacon());
            var beacon2 = await _persistence.CreateAsync(null, TestModel.CreateBeacon());
   
            var filter = FilterParams.FromTuples(
                "site_id", $"{String.Join(",", beacon2.SiteId)}",
                "udi", beacon1.Udi
            );

            var result = await _persistence.GetPageByFilterAsync(null, filter, null);

            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Count);
            TestModel.AssertEqual(beacon1, result.Data[0]);
        }
    }
}
