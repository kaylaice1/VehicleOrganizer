namespace VehicleOrganizer.Models
{
    public class Vehicle(string make = "Unknown", string model = "Unknown", string color = "Unknown")
    {
        public int Id { get; set; }
        public string Make { get; set; } = make;
        public string Model { get; set; } = model;
        public string Color { get; set; } = color;
    }
}
