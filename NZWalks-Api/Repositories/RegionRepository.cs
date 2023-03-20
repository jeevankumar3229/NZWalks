using NZWalks_Api.Data;
using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nzwalksdbcontext;

        //So I will first I would want to talk to the database first and get the regions from there.
        //I will use the constructor to basically fetch the database that we have injected in the services.
        public RegionRepository(NZWalksDbContext nzwalksdbcontext)
        {
            this.nzwalksdbcontext = nzwalksdbcontext;
        }
        public IEnumerable<Region> GetAll()
        {
            return nzwalksdbcontext.Regions.ToList();
        }
        //Now we will inject this interface and the implementation into these services again using dependency injection.
    }
}
