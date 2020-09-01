using Microsoft.EntityFrameworkCore;



namespace SmartFridge.Models
{
    public class DevicesContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<NotificationFromDevice> NotificationFromDevices { get; set; }
        public DevicesContext(DbContextOptions<DevicesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}


