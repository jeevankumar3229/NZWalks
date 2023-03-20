using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public interface IRegionRepository
    {
       Enumerable<Region> GetAll();
    }
}
