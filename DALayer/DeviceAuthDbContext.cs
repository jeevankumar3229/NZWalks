using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer
{
    public class DeviceAuthDbContext : IdentityDbContext
    {
        public DeviceAuthDbContext(DbContextOptions<DeviceAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id="6reader7",
                    Name="Reader",
                    NormalizedName="Read",
                    ConcurrencyStamp="6reader7"
                    
                },
                new IdentityRole
                {
                    Id="6writer7",
                    Name="Writer",
                    NormalizedName="Write",
                    ConcurrencyStamp="6writer7"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
