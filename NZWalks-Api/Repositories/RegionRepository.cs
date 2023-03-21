using Microsoft.EntityFrameworkCore;
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
        /*public IEnumerable<Region> GetAll()
        {
            return nzwalksdbcontext.Regions.ToList();
        }*/
        //Now we will inject this interface and the implementation into these services again using dependency injection.
        //to asynchronous
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nzwalksdbcontext.Regions.ToListAsync();
        }

        //to get region by id
        public async Task<Region> GetAsync(Guid id)
        {
            return await nzwalksdbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);//if it finds return that region otherwise null value
        }

        //to add new Region
        public async Task<Region> AddRegionAsync(Region region)
        {
            region.Id = Guid.NewGuid();//api overrides the id value set by the client
            await nzwalksdbcontext.AddAsync(region);//add it to context
            await nzwalksdbcontext.SaveChangesAsync();
            return region;
        }
        //to delete a region
        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            //check if region exists
            var region=await nzwalksdbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
           if (region == null)
            {
                return null;
            }
            //delete the region

            nzwalksdbcontext.Regions.Remove(region);
            await nzwalksdbcontext.SaveChangesAsync();
            return region;
        }
        //to update a region
        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var regions = await nzwalksdbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            regions.Code = region.Code;
            regions.Name = region.Name;
            regions.Area = region.Area;
            regions.Latitude = region.Latitude;
            regions.Longitude = region.Longitude;
            regions.Population = region.Population;
            await nzwalksdbcontext.SaveChangesAsync();

            return regions;
        }
    }
}
