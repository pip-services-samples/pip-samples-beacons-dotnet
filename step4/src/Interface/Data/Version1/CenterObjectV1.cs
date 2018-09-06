﻿using System.Runtime.Serialization;

namespace Beacons.Data.Version1
{
    [DataContract]
    public class CenterObjectV1
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "coordinates")]
        public double[] Coordinates { get; set; }
    }
}
