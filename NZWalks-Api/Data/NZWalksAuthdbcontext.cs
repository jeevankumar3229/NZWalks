using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//for identity database
namespace NZWalks_Api.Data
{
    public class NZWalksAuthdbcontext : IdentityDbContext
    {
        public NZWalksAuthdbcontext(DbContextOptions<NZWalksAuthdbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "50f2113c-29a5-4da5-9f21-a78b564cbc47";
            var writerRoleId = "d5be64cc-727b-4e8b-8d93-13e7af8d5ba7";
            var roles = new List<IdentityRole>//identity role is present in   mic....aspnetcore.identity package and it has four parameters
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),


                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),


                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
