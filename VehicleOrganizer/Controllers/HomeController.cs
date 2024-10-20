using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleOrganizer.Models; // Ensure you are using the right namespace for VehicleDbContext

namespace VehicleOrganizer.Controllers
{
    public class HomeController(VehicleDbContext context) : Controller
    {
        private readonly VehicleDbContext _context = context;

        // Index action to list vehicles
        public IActionResult Index()
        {
            var vehicles = _context.Vehicles.ToList(); // Fetch vehicles from the database
            return View(vehicles); // Pass data to the view
        }

        // Create action to display the vehicle creation form
        public IActionResult Create()
        {
            return View();
        }

        // Post action to add a new vehicle to the database
        [HttpPost]
        public IActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid) // Ensure the model is valid
            {
                _context.Vehicles.Add(vehicle); // Add vehicle to the context
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("Index"); // Redirect to the Index page
            }

            return View(vehicle); // Return the same view with the model if validation fails
        }

        // Edit action to display the vehicle edit form
        public IActionResult Edit(int id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound(); // If vehicle not found, return 404
            }
            return View(vehicle); // Return the vehicle to the view for editing
        }

        // POST: Home/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id, Make, Model, Color")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Vehicles.Any(v => v.Id == vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // Delete action to display the vehicle deletion confirmation
        public IActionResult Delete(int id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound(); // If vehicle not found, return 404
            }

            return View(vehicle); // Return the vehicle to the view for deletion
        }

        // Post action to delete the vehicle
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (vehicle == null)
            {
                return NotFound(); // If vehicle not found, return 404
            }

            _context.Vehicles.Remove(vehicle); // Remove the vehicle from the context
            _context.SaveChanges(); // Save the changes to the database
            return RedirectToAction("Index"); // Redirect to the Index page
        }
    }
}
