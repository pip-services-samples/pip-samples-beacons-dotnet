using PipServices.Container;

namespace Step6.Container
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
