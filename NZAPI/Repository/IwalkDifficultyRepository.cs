using NZAPI.Models.Domains;

namespace NZAPI.Repository
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
