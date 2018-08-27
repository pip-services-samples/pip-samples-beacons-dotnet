using PipServices.Commons.Data;
using System.Dynamic;
using System.Threading.Tasks;
using PipServices.Rpc.Clients;
using Interfaces.Data.Version1;

namespace Clients.Version1
{
    public class BeaconsHttpClientV1: CommandableHttpClient, IBeaconsClientV1
    {
        public BeaconsHttpClientV1()
            : base("v1/beacons")
        {
        }

        public async Task<BeaconV1> CreateAsync(string correlationId, BeaconV1 beacon)
        {
            return await CallCommandAsync<BeaconV1>(
                "create_beacon",
                correlationId,
                new
                {
                    beacon = beacon
                }
            );
        }

        public async Task<BeaconV1> DeleteByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<BeaconV1>(
                "delete_beacon",
                correlationId,
                new
                {
                    id = id
                }
            );
        }

        public async Task<DataPage<BeaconV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await CallCommandAsync<DataPage<BeaconV1>>(
                "get_beacons",
                correlationId,
                new
                {
                    filter = filter,
                    paging = paging
                }
            );
        }

        public async Task<BeaconV1> GetOneByIdAsync(string correlationId, string id)
        {
            return await CallCommandAsync<BeaconV1>(
                "get_beacon",
                correlationId,
                new
                {
                    id = id
                }
            );
        }

        public async Task<BeaconV1> GetOneByUdiAsync(string correlationId, string udi)
        {
            return await CallCommandAsync<BeaconV1>(
                "get_beacon_by_udi",
                correlationId,
                new
                {
                    udi = udi
                }
            );
        }

        public async Task<BeaconV1> UpdateAsync(string correlationId, BeaconV1 beacon)
        {
            return await CallCommandAsync<BeaconV1>(
                "update_beacon",
                correlationId,
                new
                {
                    beacon = beacon
                }
            );
        }

        public async Task<ExpandoObject> CalculatePosition(string correlationId, string siteId, string[] udis)
        {
            return await CallCommandAsync<ExpandoObject>(
                "calculate_position",
                correlationId,
                new
                {
                    siteId = siteId,
                    udis = udis
                }
            );
        }


    }
}
