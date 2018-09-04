using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using Service.Persistence;
using Service.Logic;
using Service.Services.Version1;
using System.Threading.Tasks;
using Xunit;
using Client.Clients.Version1;

namespace Test.Clients.Version1
{
    public class BeaconsHttpClientV1Test
    {
            private static readonly ConfigParams HttpConfig = ConfigParams.FromTuples(
                "connection.protocol", "http",
                "connection.host", "localhost",
                "connection.port", 8080
            );

            private BeaconsMemoryPersistence _persistence;
            private BeaconsController _controller;
            private BeaconsHttpClientV1 _client;
            private BeaconsHttpServiceV1 _service;
            private BeaconsClientV1Fixture _fixture;

            public BeaconsHttpClientV1Test()
            {
                _persistence = new BeaconsMemoryPersistence();
                _controller = new BeaconsController();
                _client = new BeaconsHttpClientV1();
                _service = new BeaconsHttpServiceV1();

                IReferences references = References.FromTuples(
                    new Descriptor("pip-samples-beacons", "persistence", "memory", "default", "1.0"), _persistence,
                    new Descriptor("pip-samples-beacons", "controller", "default", "default", "1.0"), _controller,
                    new Descriptor("pip-samples-beacons", "client", "http", "default", "1.0"), _client,
                    new Descriptor("pip-samples-beacons", "service", "http", "default", "1.0"), _service
                );

                _controller.SetReferences(references);

                _service.Configure(HttpConfig);
                _service.SetReferences(references);

                _client.Configure(HttpConfig);
                _client.SetReferences(references);

                _fixture = new BeaconsClientV1Fixture(_client);

                _service.OpenAsync(null).Wait();
                _client.OpenAsync(null).Wait();
            }

            [Fact]
            public async Task TestHttpClientCrudOperationsAsync()
            {
                await _fixture.TestClientCrudOperationsAsync();
            }
        }
}
