using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace logindemo.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var role = new List<IdentityRole>
            {
                new IdentityRole
                {
                  Id="2ab5daac-b3d9-4899-9d8f-d189ff7583f6",
                  Name="Readers",
                  ConcurrencyStamp="2ab5daac-b3d9-4899-9d8f-d189ff7583f6",
                  NormalizedName="READERS"

                },
                new IdentityRole
                {
                  Id="4b2bcc8b-32f8-4840-ab2d-4824abe758d6",
                  Name="Writers",
                  ConcurrencyStamp="4b2bcc8b-32f8-4840-ab2d-4824abe758d6",
                  NormalizedName="WRITERS"

                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);
        }
    }
}
