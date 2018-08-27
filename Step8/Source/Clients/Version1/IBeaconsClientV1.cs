using PipServices.Commons.Data;
using System.Dynamic;
using System.Threading.Tasks;
using Interfaces.Data.Version1;

namespace Clients.Version1
{
    public interface IBeaconsClientV1
    {
            Task<BeaconV1> GetOneByUdiAsync(string correlationId, string udi);
            Task<ExpandoObject> CalculatePosition(string correlationId, string siteId, string[] udis);           
            Task<BeaconV1> CreateAsync(string correlationId, BeaconV1 beacon);
            Task<BeaconV1> UpdateAsync(string correlationId, BeaconV1 beacon);
            Task<BeaconV1> DeleteByIdAsync(string correlationId, string id);
            Task<BeaconV1> GetOneByIdAsync(string correlationId, string id);
            Task<DataPage<BeaconV1>> GetPageByFilterAsync(string correlationId, FilterParams filter, PagingParams paging);        
    }
}
