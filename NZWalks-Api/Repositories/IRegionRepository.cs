using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public interface IRegionRepository
    {
        // IEnumerable<Region> GetAll();
        //convert to asynchronous

       Task<IEnumerable<Region>> GetAllAsync();

        //to get a region by id
        Task<Region> GetAsync(Guid id);

        Task<Region> AddRegionAsync(Region region);

        Task<Region> DeleteRegionAsync(Guid id);
    }
}
