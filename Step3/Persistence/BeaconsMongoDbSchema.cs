using MongoDB.Bson.Serialization.Attributes;
using PipServices.Commons.Data;
using Step2.Interfaces.Version1;

namespace Step3.Persistence
{
    [BsonIgnoreExtraElements]
    public class BeaconsMongoDbSchema: IStringIdentifiable
    {
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("site_id")]
        public string SiteId { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("udi")]
        public string Udi { get; set; }
        [BsonElement("label")]
        public string Label { get; set; }
        [BsonElement("center")]
        public CenterObject Center { get; set; }
        [BsonElement("radius")]
        public double Radius { get; set; }

    }
}
