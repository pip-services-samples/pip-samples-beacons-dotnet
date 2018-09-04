using PipServices.Commons.Refer;
using PipServices.Components.Build;
using Service.Persistence;
using Service.Logic;
using Service.Services.Version1;

namespace Service.Build
{
    public class BeaconsServiceFactory: Factory
    {
        public static Descriptor Descriptor = new Descriptor("pip-samples-beacons", "factory", "service", "default", "1.0");
        public static Descriptor MemoryPersistenceDescriptor = new Descriptor("pip-samples-beacons", "persistence", "memory", "*", "1.0");
        public static Descriptor MongoDbPersistenceDescriptor = new Descriptor("pip-samples-beacons", "persistence", "mongodb", "*", "1.0");
        public static Descriptor ControllerDescriptor = new Descriptor("pip-samples-beacons", "controller", "default", "*", "1.0");
        public static Descriptor HttpServiceDescriptor = new Descriptor("pip-samples-beacons", "service", "http", "*", "1.0");        


        public BeaconsServiceFactory()
        {
            RegisterAsType(MemoryPersistenceDescriptor, typeof(BeaconsMemoryPersistence));
            RegisterAsType(MongoDbPersistenceDescriptor, typeof(BeaconsMongoDbPersistence));
            RegisterAsType(ControllerDescriptor, typeof(BeaconsController));
            RegisterAsType(HttpServiceDescriptor, typeof(BeaconsHttpServiceV1));            
        }
    }
}
