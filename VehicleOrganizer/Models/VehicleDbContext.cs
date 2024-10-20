using Microsoft.EntityFrameworkCore;

namespace VehicleOrganizer.Models
{
    public class VehicleDbContext(DbContextOptions<VehicleDbContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        // Method to seed initial data
        public static void Seed(VehicleDbContext context)
        {
            if (!context.Vehicles.Any())
            {
                context.Vehicles.AddRange(
                    new Vehicle("Toyota", "Camry", "Blue"),
                    new Vehicle("Honda", "Civic", "Red"),
                    new Vehicle("Ford", "F-150", "Black")
                );
                context.SaveChanges();
            }
        }
    }
}