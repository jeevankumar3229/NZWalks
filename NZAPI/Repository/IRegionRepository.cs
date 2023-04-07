using NZAPI.Models.Domains;

namespace NZAPI.Repository
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        //to get a region by id
        Task<Region?> GetAsync(Guid id);//can be nullable

        Task<Region> AddRegionAsync(Region region);

        Task<Region?> DeleteRegionAsync(Guid id);//can be nullable

        Task<Region?> UpdateRegionAsync(Guid id, Region region);
    }
}
