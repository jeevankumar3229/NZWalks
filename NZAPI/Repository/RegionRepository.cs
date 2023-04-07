using Microsoft.EntityFrameworkCore;
using NZAPI.Data;
using NZAPI.Models.Domains;

namespace NZAPI.Repository
{
    public class RegionRepository:IRegionRepository
    {
        private readonly NZWalksDbsContext nzwalksdbcontext;

        
        public RegionRepository(NZWalksDbsContext nzwalksdbcontext)
        {
            this.nzwalksdbcontext = nzwalksdbcontext;
        }
       
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            
     
                return await nzwalksdbcontext.RegionData.ToListAsync();
            
       
            
                
            
        }

        //to get region by id
        public async Task<Region?> GetAsync(Guid id)
        {
            return await nzwalksdbcontext.RegionData.FirstOrDefaultAsync(x => x.Id == id);//if it finds return that region otherwise null value
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
        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            //check if region exists
            var region = await nzwalksdbcontext.RegionData.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            //delete the region

            nzwalksdbcontext.RegionData.Remove(region);
            await nzwalksdbcontext.SaveChangesAsync();
            return region;
        }
        //to update a region
        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
            var regions = await nzwalksdbcontext.RegionData.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            regions.Code = region.Code;
            regions.Name = region.Name;
            regions.RegionImageURL = region.RegionImageURL;

            await nzwalksdbcontext.SaveChangesAsync();

            return regions;
        }
    }
}
