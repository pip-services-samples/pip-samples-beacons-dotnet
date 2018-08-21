using System.Collections.Generic;
using PipServices.Commons.Refer;
using Xunit;
using PipServices.Commons.Data;
using Step2.Interfaces.Version1;
using Step3.Persistence;
using PipServices.Commons.Config;

namespace Step4.Logic
{
    public class BeaconsControllerTest
    {
        private BeaconsController _controller;

        private BeaconsMemoryPersistence _persistence;

        public BeaconsControllerTest()
        {
            var references = new References();
            _controller = new BeaconsController();

            _persistence = new BeaconsMemoryPersistence();
            _persistence.Configure(new ConfigParams());

            references.Put(new Descriptor("trainings-beacons", "persistence", "memory", "*", "1.0"), _persistence);
            references.Put(new Descriptor("trainings-beacons", "controller", "default", "*", "1.0"), _controller);

            _controller.SetReferences(references);
        }

        [Fact]
        public async void It_Should_Create_Task_Async()
        {
            var beacon = TestModel.CreateBeacon();

            var result = await _controller.CreateAsync(null, beacon);

            Assert.Equal(beacon.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Update_Task_Async()
        {
            var beacon = TestModel.CreateBeacon();

            var result = await _controller.UpdateAsync(null, beacon);

            Assert.Equal(beacon.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Delete_Promotion_Async()
        {
            var beacon = TestModel.CreateBeacon();

            var result = await _controller.DeleteByIdAsync(null, beacon.Id);

            Assert.Equal(beacon.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Get_Task_Async()
        {
            var beacon = TestModel.CreateBeacon();

            var result = await _controller.GetOneByIdAsync(null, beacon.Id);

            Assert.Equal(beacon.Id, result.Id);
        }

        [Fact]
        public async void It_Should_Get_Tasks_Async()
        {
            var beacons = new DataPage<BeaconV1>()
            {
                Data = new List<BeaconV1>() { TestModel.CreateBeacon(), TestModel.CreateBeacon(), TestModel.CreateBeacon() },
                Total = 3
            };
            var filter = new FilterParams();
            var paging = new PagingParams();

            var result = await _controller.GetPageByFilterAsync(null, filter, paging);

            Assert.Equal(beacons.Total, beacons.Total);
        }
    }
}
