using Build;
using PipServices.Container;

namespace Container
{
    public class BeaconsProcess: ProcessContainer
    {
        public BeaconsProcess()
            : base("beacons", "Beacons microservice")
        {
            _factories.Add(new BeaconsServiceFactory());
        }
    }
}
