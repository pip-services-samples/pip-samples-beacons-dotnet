using Step2.Interfaces.Version1;
using System.Threading.Tasks;
using Xunit;

namespace Step7.Clients.Version1
{
    public class BeaconsClientV1Fixture
    {
        private IBeaconsClientV1 _client;

        public BeaconsClientV1Fixture(IBeaconsClientV1 client)
        {
            _client = client;
        }

        public async Task TestClientCrudOperationsAsync()
        {
            var beacon1 = await _client.CreateAsync(null, TestModel.CreateBeacon());
            var beacon2 = await _client.CreateAsync(null, TestModel.CreateBeacon());

            var page = await _client.GetPageByFilterAsync(null, null, null);
            Assert.Equal(2, page.Data.Count);

            await _client.DeleteByIdAsync(null, beacon1.Id);

            var result = await _client.GetOneByIdAsync(null, beacon1.Id);
            Assert.Null(result);

            beacon2.Label = "New Label ";
            beacon2.Radius = 11.0;

            result = await _client.UpdateAsync(null, beacon2);
            TestModel.AssertEqual(beacon2, result);

            await _client.DeleteByIdAsync(null, beacon2.Id);
            page = await _client.GetPageByFilterAsync(null, null, null);
            Assert.Empty(page.Data);
        }
    }
}
