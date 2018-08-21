using Step2.Interfaces.Version1;
using PipServices.Commons.Commands;
using PipServices.Commons.Config;
using PipServices.Commons.Data;
using PipServices.Commons.Refer;
using Step3.Persistence;
using System.Threading.Tasks;
using PipServices.Components.Logic;

namespace Step4.Logic
{
    public class BeaconsController: AbstractController, ICommandable, IBeaconsController
    {
        private IBeaconsPersistence _Persistence;
        //private BeaconsCommandSet _CommandSet;

        public override string Component { get { return "Trainings.Beacons"; } }

        public BeaconsController()
        {
            _dependencyResolver = new DependencyResolver(ConfigParams.FromTuples("dependencies.persistence", "trainings-beacons:persistence:*:*:1.0"));
        }

        public override void SetReferences(IReferences references)
        {
            base.SetReferences(references);

            _Persistence = _dependencyResolver.GetOneRequired<IBeaconsPersistence>("persistence");
        }

        // TO DO
        public CommandSet GetCommandSet()
        {
            return new CommandSet();// _CommandSet ?? new BeaconsCommandSet(this));
        }

        public async Task<BeaconV1> CreateAsync(string correlationId, BeaconV1 beacon)
        {
            return await SafeInvokeAsync(correlationId, "CreateAsync", () =>
            {
                return _Persistence.CreateAsync(correlationId, beacon);
            });
        }

        public async Task<BeaconV1> UpdateAsync(string correlationId, BeaconV1 beacon)
        {
            return await SafeInvokeAsync(correlationId, "UpdateAsync", () =>
            {
                return _Persistence.UpdateAsync(correlationId, beacon);
            });
        }

        public async Task<BeaconV1> DeleteByIdAsync(string correlationId, string id)
        {
            return await SafeInvokeAsync(correlationId, "DeleteAsync", () =>
            {
                return _Persistence.DeleteByIdAsync(correlationId, id);
            });
        }

        public async Task<BeaconV1> GetOneByIdAsync(string correlationId, string id)
        {
            return await SafeInvokeAsync(correlationId, "GetOneByIdAsync", () =>
            {
                return _Persistence.GetOneByIdAsync(correlationId, id);
            });
        }

        public async Task<DataPage<BeaconV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            return await SafeInvokeAsync(correlationId, "GetPageByFilterAsync", () =>
            {
                return _Persistence.GetPageByFilterAsync(correlationId, filter, paging);
            });
        }

        public async Task<BeaconV1> GetOneByUdiAsync(string correlationId, string udi)
        {
            return await SafeInvokeAsync(correlationId, "GetOneByUdiAsync", () =>
            {
                return _Persistence.GetOneByUdiAsync(correlationId, udi);
            });
        }

        public async Task<CenterObject> CalculatePosition(string correlationId, string siteId, string[] udis)
        {
            BeaconV1[] beacons;
            CenterObject position = null;

            if (udis == null || udis.Length == 0)
            {
                return null;
            }

            var result = await this._Persistence.GetPageByFilterAsync(correlationId, FilterParams.FromTuples("site_id", siteId, "udis", udis), null);
            beacons = result.Data.ToArray();
            var lat = 0;
            var lng = 0;
            var count = 0;

            foreach (var beacon in beacons)
            {
                if (beacon.Center != null && beacon.Center.GetType().ToString() == "Point"
                    && beacon.Center.Coordinates.Length > 1)
                {
                    lng += beacon.Center.Coordinates[0];
                    lat += beacon.Center.Coordinates[1];
                    count += 1;
                }
            }
            if (count > 0)
            {
                position = new CenterObject("Point", new int[] { lng / count, lat / count });
            }
            return position;
        }
    }
}
