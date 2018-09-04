using Interface.Data.Version1;
using PipServices.Commons.Random;
using System.Collections.Generic;
using Xunit;

namespace Test.Interface.Data.Version1
{
    public class TestModel
    {
        public static BeaconV1 CreateBeacon()
        {
            return new BeaconV1()
            {
                Id = RandomString.NextString(10, 20),
                SiteId = RandomString.NextString(10, 20),
                Type = RandomText.Word(),
                Udi = RandomString.NextString(10, 20),
                Label = RandomText.Word(),
                Center = new CenterObject(RandomText.Word(), new int[] {RandomInteger.NextInteger(1, 10), RandomInteger.NextInteger(1, 10) }),
                Radius = RandomDouble.NextDouble(100.0)
            };
        }

        public static BeaconV1[] CreateBeacons()
        {
            var result = new List<BeaconV1>();
            var count = RandomInteger.NextInteger(1, 5);
            for (var i = 0; i < count; i++)
            {
                result.Add(CreateBeacon());
            }
            return result.ToArray();
        }

        public static void AssertEqual(BeaconV1 expectedBeacon, BeaconV1 actualBeacon)
        {
            Assert.Equal(expectedBeacon.Id,     actualBeacon.Id);
            Assert.Equal(expectedBeacon.SiteId, actualBeacon.SiteId);
            Assert.Equal(expectedBeacon.Type,   actualBeacon.Type);
            Assert.Equal(expectedBeacon.Udi,    actualBeacon.Udi);
            Assert.Equal(expectedBeacon.Label,  actualBeacon.Label);
            Assert.Equal(expectedBeacon.Center, actualBeacon.Center);
            Assert.Equal(expectedBeacon.Radius, actualBeacon.Radius);

        }
    }
}
