using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Models.Domains;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace NZWalks_Api.Repositories
{
    public class WalkRepository:IWalkRepository
    {
        private readonly NZWalksDbContext nzwalksdbcontext;

        public WalkRepository(NZWalksDbContext nzwalksdbcontext)
        {
            this.nzwalksdbcontext = nzwalksdbcontext;
        }

        
        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await nzwalksdbcontext.Walks //I also want to fetch the region details that the work is in.So I will use this keyword "include" and using include basically
                .Include(x => x.Region)         //You can tell  entity framework what to include when fetching the results .
                .Include(x => x.WalkDifficulty) //So it will fetch it knows all the relationship between the walks and region.So it will only fetch the region which this work 
                .ToListAsync();                 // is in because  it has a "regionID" and along with the region it will also fetch walkDifficulty because it has a property 
                                                //walkdifficultyId.

        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            return await nzwalksdbcontext.Walks.
                Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
               
        }

        //add a new walk
        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            //assign a new id-not asking id from user
            walk.Id = Guid.NewGuid();
            await nzwalksdbcontext.Walks.AddAsync(walk);
            await nzwalksdbcontext.SaveChangesAsync();
            return walk;
        }

        //to update a walk
        public async Task<Walk> UpdateWalkAsync(Guid id,Walk walk)
        {
            var walks = await nzwalksdbcontext.Walks.FindAsync(id);
            if (walks != null)
            {
                walks.Name = walk.Name;
                walks.Length = walk.Length;
                walks.RegionId = walk.RegionId;
                walks.WalkDifficultyId = walk.WalkDifficultyId;
                await nzwalksdbcontext.SaveChangesAsync();
                return walks;
            }

            return null;

            
        }

        //delete walk
        public async Task<Walk> DeleteWalkAsync(Guid id)
        {
            var walk = await nzwalksdbcontext.Walks.FindAsync(id);
            if (walk != null)
            {
                nzwalksdbcontext.Walks.Remove(walk);
                await nzwalksdbcontext.SaveChangesAsync();
                return walk;
            }
            return null;
        }
    }
}
