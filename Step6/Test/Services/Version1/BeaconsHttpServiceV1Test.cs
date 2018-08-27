using PipServices.Commons.Config;
using PipServices.Commons.Convert;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using Interfaces.Data.Version1;
using Persistence;
using Logic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Services.Version1;
using Test.Interfaces.Data.Version1;

namespace Test.Services.Version1
{
    public class BeaconsHttpServiceV1Test
    {
        private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
            "connection.protocol", "http",
            "connection.host", "localhost",
            "connection.port", "8080"
        );

        private BeaconsMemoryPersistence _persistence;
        private BeaconsController _controller;
        private BeaconsHttpServiceV1 _service;

        public BeaconsHttpServiceV1Test()
        {
            _persistence = new BeaconsMemoryPersistence();
            _controller = new BeaconsController();
            _service = new BeaconsHttpServiceV1();

            IReferences references = References.FromTuples(
                new Descriptor("pip-samples-beacons", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("pip-samples-beacons", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("pip-samples-beacons", "service", "http", "default", "1.0"), _service
            );

            _controller.SetReferences(references);

            _service.Configure(HttpConfig);
            _service.SetReferences(references);
            _service.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task It_Should_Test_Crud_Operations()
        {
            var expectedBeacon1 = TestModel.CreateBeacon();
            var beacon1 = await Invoke<BeaconV1>("create_beacon", new { beacon = expectedBeacon1 });
            TestModel.AssertEqual(expectedBeacon1, beacon1);

            var expectedBeacon2 = TestModel.CreateBeacon();
            var beacon2 = await Invoke<BeaconV1>("create_beacon", new { beacon = expectedBeacon1 });
            TestModel.AssertEqual(expectedBeacon1, beacon2);

            var page = await Invoke<DataPage<BeaconV1>>("get_beacons", new { });
            Assert.NotNull(page);
            Assert.Equal(2, page.Data.Count);

            beacon1.Radius = 25.0;
            beacon2.Radius = 50.0;

            var beacon = await Invoke<BeaconV1>("update_beacon", new { task = beacon1 });
            TestModel.AssertEqual(beacon1, beacon);

            beacon = await Invoke<BeaconV1>("delete_beacon", new { id = beacon1.Id });
            Assert.NotNull(beacon);
            Assert.Equal(beacon1.Id, beacon.Id);

            beacon = await Invoke<BeaconV1>("get_beacon", new { id = beacon1.Id });
            Assert.Null(beacon);

            beacon = await Invoke<BeaconV1>("delete_beacon", new { id = beacon2.Id });
            Assert.NotNull(beacon);
            Assert.Equal(beacon2.Id, beacon.Id);

        }

        private static async Task<T> Invoke<T>(string route, dynamic request)
        {
            using (var httpClient = new HttpClient())
            {
                var requestValue = JsonConverter.ToJson(request);
                using (var content = new StringContent(requestValue, Encoding.UTF32, "application/json"))
                {
                    var response = await httpClient.PostAsync("http://localhost:27017/api/v1/beacons/" + route, content);
                    var responseValue = response.Content.ReadAsStringAsync().Result;
                    return JsonConverter.FromJson<T>(responseValue);
                }
            }
        }
    }
}
