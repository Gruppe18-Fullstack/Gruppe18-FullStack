using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gruppe18_FullStack.Data;
using Gruppe18_FullStack.Models;

namespace Gruppe18_FullStack.Controllers
{
    public class WeatherStationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeatherStationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeatherStations
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeatherStations.ToListAsync());
        }

        // GET: WeatherStations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherStation = await _context.WeatherStations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherStation == null)
            {
                return NotFound();
            }

            return View(weatherStation);
        }

        // GET: WeatherStations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeatherStations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Latitude,Longitude")] WeatherStation weatherStation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weatherStation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weatherStation);
        }

        // GET: WeatherStations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherStation = await _context.WeatherStations.FindAsync(id);
            if (weatherStation == null)
            {
                return NotFound();
            }
            return View(weatherStation);
        }

        // POST: WeatherStations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Latitude,Longitude")] WeatherStation weatherStation)
        {
            if (id != weatherStation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherStation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherStationExists(weatherStation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(weatherStation);
        }

        // GET: WeatherStations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherStation = await _context.WeatherStations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherStation == null)
            {
                return NotFound();
            }

            return View(weatherStation);
        }

        // POST: WeatherStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weatherStation = await _context.WeatherStations.FindAsync(id);
            if (weatherStation != null)
            {
                _context.WeatherStations.Remove(weatherStation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherStationExists(int id)
        {
            return _context.WeatherStations.Any(e => e.Id == id);
        }
    }
}
