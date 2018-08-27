using MongoDB.Driver;
using PipServices.Commons.Data;
using System.Threading.Tasks;
using PipServices.MongoDb.Persistence;
using AutoMapper;
using Interfaces.Data.Version1;

namespace Persistence
{
    public class BeaconsMongoDbPersistence: IdentifiableMongoDbPersistence<BeaconsMongoDbSchema, string>, IBeaconsPersistence
    {
        public BeaconsMongoDbPersistence() : base("beacons") { }

        private BeaconV1 ToPublic(BeaconsMongoDbSchema val)
        {
            if (val == null)
            {
                return null;
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BeaconsMongoDbSchema, BeaconV1>());
            var mapper = config.CreateMapper();
            BeaconV1 result = mapper.Map<BeaconV1>(val);
            return result;
        }

        private static BeaconsMongoDbSchema FromPublic(BeaconV1 val)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<BeaconV1, BeaconsMongoDbSchema>());
            var mapper = config.CreateMapper();
            BeaconsMongoDbSchema result = mapper.Map<BeaconsMongoDbSchema>(val);
            return result;
        }

        public async Task<BeaconV1> GetOneByUdiAsync(string correlationId, string udi)
        {
            return await GetOneByUdiAsync(correlationId, udi);
        }

        public async Task<DataPage<BeaconV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging)
        {
            var result = await GetPageByFilterAsync(correlationId, ComposeFilter(filter), paging);
            var data = result.Data.ConvertAll<BeaconV1>(x => ToPublic(x));

            return new DataPage<BeaconV1>()
            {
                Data = data,
                Total = data.Count
            };
        }

        public async Task<BeaconV1> GetOneByIdAsync(string correlationId, string id)
        {
            return await GetOneByIdAsync(correlationId, id);
        }

        public async Task<BeaconV1> CreateAsync(string correlationId, BeaconV1 beacon)
        {
            var result = await CreateAsync(correlationId, FromPublic(beacon));

            return ToPublic(result);
        }

        public async Task<BeaconV1> UpdateAsync(string correlationId, BeaconV1 beacon)
        {
            var result = await UpdateAsync(correlationId, FromPublic(beacon));

            return ToPublic(result);
        }

        public async Task<BeaconV1> DeleteByIdAsync(string correlationId, string id)
        {
            var result = await DeleteByIdAsync(correlationId, id);

            return result;
        }

        private FilterDefinition<BeaconsMongoDbSchema> ComposeFilter(FilterParams filterParams)
        {
            filterParams = filterParams ?? new FilterParams();

            var builder = Builders<BeaconsMongoDbSchema>.Filter;
            var filter = builder.Empty;

            foreach (var filterKey in filterParams.Keys)
            {
                if (filterKey.Equals("udi"))
                {
                    filter &= builder.In(s => s.Id, filterParams.GetAsArray("udi"));
                    continue;
                }

                filter &= builder.Eq(filterKey, filterParams[filterKey]);
            }

            return filter;
        }

    }
}

