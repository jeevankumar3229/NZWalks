using Microsoft.EntityFrameworkCore;
using NZAPI.Data;

namespace NZAPI.Repository
{
    public class WalkDifficultyRepository : IwalkDifficultyRepository
    {
        private readonly NZWalksDbsContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbsContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Models.Domains.WalkDifficulty> AddWalkDifficultyAsync(Models.Domains.WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficultyData.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<Models.Domains.WalkDifficulty?> DeleteWalkDiffAsync(Guid id)
        {
            var walkDiff = await nZWalksDbContext.WalkDifficultyData.FindAsync(id);
            if (walkDiff != null)
            {
                nZWalksDbContext.WalkDifficultyData.Remove(walkDiff);
                await nZWalksDbContext.SaveChangesAsync();
                return walkDiff;
            }
            return null;
        }

        public async Task<IEnumerable<Models.Domains.WalkDifficulty>> GetWalkDiffAsync()
        {
            return await nZWalksDbContext.WalkDifficultyData.ToListAsync();
        }

        public async Task<Models.Domains.WalkDifficulty?> GetWalkDiffById(Guid id)
        {
            return await nZWalksDbContext.WalkDifficultyData.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Models.Domains.WalkDifficulty?> UpdateWalkDiffAsync(Guid id, Models.Domains.WalkDifficulty walkDifficulty)
        {
            var walkDiff = await nZWalksDbContext.WalkDifficultyData.FindAsync(id);
            if (walkDiff != null)
            {
                walkDiff.Name = walkDifficulty.Name;
                await nZWalksDbContext.SaveChangesAsync();
                return walkDiff;
            }
            return null;
        }
    }
}
