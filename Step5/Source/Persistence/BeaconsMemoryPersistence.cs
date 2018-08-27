using System.Collections.Generic;
using PipServices.Commons.Data;
using System.Threading.Tasks;
using System.Linq;
using PipServices.Data.Persistence;
using Interfaces.Data.Version1;

namespace Persistence
{
    public class BeaconsMemoryPersistence : IdentifiableMemoryPersistence<BeaconV1, string>, IBeaconsPersistence
    {
        private const int MaxPageSize = 100;
        private object _lock = new object();
        private Dictionary<string, BeaconV1> _beacons = new Dictionary<string, BeaconV1>();
                
        public BeaconsMemoryPersistence()
        {
             this._maxPageSize = 1000;
        }

        public async Task<BeaconV1> CreateAsync(string correlationId, BeaconV1 beacon)
        {
            beacon.Id = beacon.Id ?? IdGenerator.NextLong();

            lock (_lock)
            {
                _beacons[beacon.Id] = beacon;
            }
            return await Task.FromResult(beacon);
        }

        public async Task<BeaconV1> DeleteByIdAsync(string correlationId, string id)
        {
            BeaconV1 result = null;

            lock (_lock)
            {
                _beacons.TryGetValue(id, out result);
                if (result != null)
                {
                    _beacons.Remove(id);
                }
            }
            return await Task.FromResult(result);
        }

        public async Task<BeaconV1> UpdateAsync(string correlationId, BeaconV1 beacon)
        {
            lock (_lock)
            {
                _beacons[beacon.Id] = beacon;
            }

            return await Task.FromResult(beacon);
        }

        public Task<DataPage<BeaconV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            filter = filter ?? new FilterParams();
            var site_id = filter.GetAsNullableString("site_id");

            lock (_lock)
            {
                var foundBeacons = new List<BeaconV1>();

                foreach (var beacon in _beacons.Values)
                {
                    if (site_id != null && !site_id.Contains(beacon.SiteId))
                    {
                        continue;
                    }
                    foundBeacons.Add(beacon);
                }

                paging = paging ?? new PagingParams();
                var skip = paging.GetSkip(0);
                var take = paging.GetTake(MaxPageSize);
                var page = foundBeacons.Skip((int)skip).Take((int)take).ToList();
                var total = foundBeacons.Count;

                return Task.FromResult(new DataPage<BeaconV1>(page, total));
            }
        }

        public async Task<BeaconV1> GetOneByIdAsync(string correlationId, string id)
        {
            BeaconV1 result = null;

            lock (_lock)
            {
                _beacons.TryGetValue(id, out result);
            }

            return await Task.FromResult(result);
        }

        public async Task<BeaconV1> GetOneByUdiAsync(string CorrelationId, string udi)
        {
            BeaconV1 result = null;

            lock (_lock)
            {
                _beacons.TryGetValue(udi, out result);
            }

            return await Task.FromResult(result);
        }
    }
}
