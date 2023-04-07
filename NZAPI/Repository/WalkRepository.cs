using Microsoft.EntityFrameworkCore;
using NZAPI.Data;
using NZAPI.Models.Domains;

namespace NZAPI.Repository
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbsContext nzwalksdbcontext;

        public WalkRepository(NZWalksDbsContext nzwalksdbcontext)
        {
            this.nzwalksdbcontext = nzwalksdbcontext;
        }


        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            


            return await nzwalksdbcontext.WalkData.Include("Regions").Include("WalkDifficulty").ToListAsync();




        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
            return await nzwalksdbcontext.WalkData.
                Include("Regions")//Regions is a property name in walk not a Region class
                .Include("WalkDifficulty")
                .FirstOrDefaultAsync(x => x.Id == id);

        }

        //add a new walk
        public async Task<Walk> AddWalkAsync(Walk walk)
        {
            //assign a new id-not asking id from user
            walk.Id = Guid.NewGuid();
            await nzwalksdbcontext.WalkData.AddAsync(walk);
            await nzwalksdbcontext.SaveChangesAsync();
            return walk;
        }

        //to update a walk
        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walks = await nzwalksdbcontext.WalkData.FindAsync(id);
            if (walks != null)
            {
                walks.Name = walk.Name;
                walks.Length = walk.Length;
                walks.Description = walk.Description;
                walks.WalkImageURL = walk.WalkImageURL;
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
            var walk = await nzwalksdbcontext.WalkData.FindAsync(id);
            if (walk != null)
            {
                nzwalksdbcontext.WalkData.Remove(walk);
                await nzwalksdbcontext.SaveChangesAsync();
                return walk;
            }
            return null;
        }
    }
}
