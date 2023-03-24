using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public interface IwalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetWalkDiffAsync();

        Task<WalkDifficulty?> GetWalkDiffById(Guid id);

        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty);

        Task<WalkDifficulty?> UpdateWalkDiffAsync(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty?> DeleteWalkDiffAsync(Guid id);
    }
}
