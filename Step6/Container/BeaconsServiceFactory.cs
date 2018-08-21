using PipServices.Commons.Refer;
using PipServices.Components.Build;
using Step4.Logic;
using Step3.Persistence;
using Step5.Services.Version1;

namespace Step6.Container
{
    public class BeaconsServiceFactory: Factory
    {
        public static Descriptor Descriptor = new Descriptor("iqs-services-beacons", "factory", "service", "default", "1.0");
        public static Descriptor MemoryPersistenceDescriptor = new Descriptor("iqs-services-beacons", "persistence", "memory", "*", "1.0");
        public static Descriptor MongoDbPersistenceDescriptor = new Descriptor("iqs-services-beacons", "persistence", "mongodb", "*", "1.0");
        public static Descriptor ControllerDescriptor = new Descriptor("iqs-services-beacons", "controller", "default", "*", "1.0");
        public static Descriptor HttpServiceDescriptor = new Descriptor("iqs-services-beacons", "service", "http", "*", "1.0");


        public BeaconsServiceFactory()
        {
            RegisterAsType(MemoryPersistenceDescriptor, typeof(BeaconsMemoryPersistence));
            RegisterAsType(MongoDbPersistenceDescriptor, typeof(BeaconsMongoDbPersistence));
            RegisterAsType(ControllerDescriptor, typeof(BeaconsController));
            RegisterAsType(HttpServiceDescriptor, typeof(BeaconsHttpServiceV1));            
        }
    }
}
