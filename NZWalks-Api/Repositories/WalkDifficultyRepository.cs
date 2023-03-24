using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Models.Domains;
using NZWalks_Api.Models.DTOs;

namespace NZWalks_Api.Repositories
{
    public class WalkDifficultyRepository:IwalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Models.Domains.WalkDifficulty> AddWalkDifficultyAsync(Models.Domains.WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<Models.Domains.WalkDifficulty?> DeleteWalkDiffAsync(Guid id)
        {
            var walkDiff = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (walkDiff != null)
            {
                nZWalksDbContext.WalkDifficulty.Remove(walkDiff);
                await nZWalksDbContext.SaveChangesAsync();
                return walkDiff;
            }
            return null;
        }

        public async Task<IEnumerable<Models.Domains.WalkDifficulty>> GetWalkDiffAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<Models.Domains.WalkDifficulty?> GetWalkDiffById(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Models.Domains.WalkDifficulty?> UpdateWalkDiffAsync(Guid id, Models.Domains.WalkDifficulty walkDifficulty)
        {
             var walkDiff=await nZWalksDbContext.WalkDifficulty.FindAsync(id);
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
