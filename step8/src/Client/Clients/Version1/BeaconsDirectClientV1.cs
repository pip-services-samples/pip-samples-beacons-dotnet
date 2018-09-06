﻿using System.Threading.Tasks;
using Beacons.Clients.Version1;
using Beacons.Data.Version1;
using Beacons.Logic;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using PipServices.Rpc.Clients;

namespace Beacons.Clients.Version1
{
    public class BeaconsDirectClientV1 : DirectClient<IBeaconsController>, IBeaconsClientV1
    {
        public BeaconsDirectClientV1() : base()
        {
            _dependencyResolver.Put("controller", new Descriptor("beacons", "controller", "*", "*", "1.0"));
        }

        public async Task<DataPage<BeaconV1>> GetBeaconsAsync(
            string correlationId, FilterParams filter, PagingParams paging)
        {
            using (Instrument(correlationId, "beacons.get_beacons"))
            {
                return await _controller.GetBeaconsAsync(correlationId, filter, paging);
            }
        }

        public async Task<BeaconV1> GetBeaconByIdAsync(string correlationId, string id)
        {
            using (Instrument(correlationId, "beacons.get_beacon_by_id"))
            {
                return await _controller.GetBeaconByIdAsync(correlationId, id);
            }
        }

        public async Task<BeaconV1> GetBeaconByUdiAsync(string correlationId, string udi)
        {
            using (Instrument(correlationId, "beacons.get_beacon_by_udi"))
            {
                return await _controller.GetBeaconByUdiAsync(correlationId, udi);
            }
        }

        public async Task<CenterObjectV1> CalculatePositionAsync(string correlationId, string siteId, string[] udis)
        {
            using (Instrument(correlationId, "beacons.calculate_position"))
            {
                return await _controller.CalculatePositionAsync(correlationId, siteId, udis);
            }
        }

        public async Task<BeaconV1> CreateBeaconAsync(string correlationId, BeaconV1 beacon)
        {
            using (Instrument(correlationId, "beacons.create_beacon"))
            {
                return await _controller.CreateBeaconAsync(correlationId, beacon);
            }
        }

        public async Task<BeaconV1> UpdateBeaconAsync(string correlationId, BeaconV1 beacon)
        {
            using (Instrument(correlationId, "beacons.update_beacon"))
            {
                return await _controller.UpdateBeaconAsync(correlationId, beacon);
            }
        }

        public async Task<BeaconV1> DeleteBeaconByIdAsync(string correlationId, string id)
        {
            using (Instrument(correlationId, "beacons.delete_beacon_by_id"))
            {
                return await _controller.DeleteBeaconByIdAsync(correlationId, id);
            }
        }
    }
}
