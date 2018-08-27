using PipServices.Commons.Refer;
using PipServices.Rpc.Services;

namespace Services.Version1
{
    public class BeaconsHttpServiceV1: CommandableHttpService
    {
        public BeaconsHttpServiceV1()
            : base("api/v1/beacons")
        {
            _dependencyResolver.Put("controller", new Descriptor("pip-samples-beacons", "controller", "default", "*", "1.0"));
        }
    }
}
