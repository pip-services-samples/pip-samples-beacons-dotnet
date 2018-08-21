using Step2.Interfaces.Version1;
using PipServices.Commons.Data;
using System.Threading.Tasks;

namespace Step4.Logic
{
    public interface IBeaconsController
    {
        Task<DataPage<BeaconV1>> GetPageByFilterAsync(string CorrelationId, FilterParams Filter, PagingParams Paging);
        Task<BeaconV1> GetOneByIdAsync(string CorrelationId, string Id);
        Task<BeaconV1> GetOneByUdiAsync(string CorrelationId, string Udi);
        Task<CenterObject> CalculatePosition(string CorrelationId, string SiteId, string[] Udis);
        Task<BeaconV1> CreateAsync(string CorrelationId, BeaconV1 Item);
        Task<BeaconV1> UpdateAsync(string CorrelationId, BeaconV1 Item);
        Task<BeaconV1> DeleteByIdAsync(string CorrelationId, string Id);
    }
}
