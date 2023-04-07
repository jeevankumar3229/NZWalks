
using Microsoft.EntityFrameworkCore;
using Models.Requests;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DALayer
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options)
        {
        }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}