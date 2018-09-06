using Beacons.Build;
using PipServices.Container;
using PipServices.Rpc.Build;

namespace Beacons.Container
{
    public class BeaconsProcess : ProcessContainer
    {
        public BeaconsProcess()
            : base("beacons", "Beacons microservice")
        {
            _factories.Add(new DefaultRpcFactory());
            _factories.Add(new BeaconsServiceFactory());
        }
    }
}
