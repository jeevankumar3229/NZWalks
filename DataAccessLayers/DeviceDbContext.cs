using DataAccessLayers.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccessLayers
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}