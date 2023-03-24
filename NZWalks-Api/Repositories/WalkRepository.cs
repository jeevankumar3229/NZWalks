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

        
        public async Task<IEnumerable<Walk>> GetAllWalksAsync(string? filteron=null, string? filterquery=null,string? sortBy=null,bool IsAscending=true,int pageno=1
            ,int pagesize=1000)
        {
            //for normal without query parameter passed
            /*  return await nzwalksdbcontext.Walks. //I also want to fetch the region details that the work is in.So I will use this keyword "include" and using include basically
                  .Include(x => x.Regions)//Regions is a property name in walk not a Region class     //You can tell  entity framework what to include when fetching the results .
             .Include(x => x.Regions)//So it will fetch it knows all the relationship between the walks and region.So it will only fetch the region which this work                                                           
                  .Include(x => x.WalkDifficulty) //is in because  it has a "regionID" and along with the region it will also fetch walkDifficulty because it has a property 
                  .ToListAsync();                 //walkdifficultyId. 
            */


            var walk = nzwalksdbcontext.Walks.Include("Regions").Include("WalkDifficulty").AsQueryable();
            //So at the moment I have a """AsQuerable()""" which I can use to apply filtering and sorting.And finally, when I'm done, I will return the result as list.

            //filtering
            //first check whether query parameters are null
            if(string.IsNullOrWhiteSpace(filteron)==false && string.IsNullOrWhiteSpace(filterquery) == false)
            {
                if (filteron.Equals("Name", StringComparison.OrdinalIgnoreCase))//check which column and ignoring the case
                {
                    walk = walk.Where(x => x.Name.Contains(filterquery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))//check which column and ignoring the case
                {
                    walk = IsAscending ? walk.OrderBy(x => x.Name) : walk.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))//check which column and ignoring the case
                {
                    walk = IsAscending ? walk.OrderBy(x => x.Length) : walk.OrderByDescending(x => x.Length);
                }
            }

            //pagination
            //Pagination is really easy to implement.It's based on the formula that you have to skip X number of results and then take Y results.
            //skip results
            var skipresults = (pageno - 1) * pagesize;

            //skip and take are methods to skip and take


            return await walk.Skip(skipresults).Take(pagesize).ToListAsync();
           
            


        }

        public async Task<Walk?> GetWalkById(Guid id)
        {
           /*return await nzwalksdbcontext.Walks.
                Include(x=>x.Region)
                .Include(x=>x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);*/
            //or
            return await nzwalksdbcontext.Walks.
                Include("Regions")//Regions is a property name in walk not a Region class
                .Include("WalkDifficulty")
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
