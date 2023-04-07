using DeviceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
    }
}