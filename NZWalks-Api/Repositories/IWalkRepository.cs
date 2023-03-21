using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkById(Guid id);
    }
}
