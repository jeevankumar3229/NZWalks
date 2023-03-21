using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkById(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
