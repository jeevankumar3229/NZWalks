using NZWalks_Api.Models.Domains;

namespace NZWalks_Api.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync(string? filteron=null, string? filterquery=null,string? sortby=null,bool IsAscending=true,int pageno=1,int pagesize=1000);//by default null because everytime you do not need to filter

        Task<Walk?> GetWalkById(Guid id);

        Task<Walk> AddWalkAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid id, Walk walk);

        Task<Walk> DeleteWalkAsync(Guid id);
    }
}
