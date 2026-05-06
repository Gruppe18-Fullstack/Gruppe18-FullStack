using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gruppe18_FullStack.Data;
using Gruppe18_FullStack.Models;
using Gruppe18_FullStack.Services;
using System.Runtime.CompilerServices;

namespace Gruppe18_FullStack.Controllers
{
    public class WeatherReadingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly WeatherApiService _weatherApiService;

        public WeatherReadingsController(ApplicationDbContext context, WeatherApiService weatherApiService)
        {
            _context = context;
            _weatherApiService = weatherApiService;
        }

        // GET: WeatherReadings
        public async Task<IActionResult> Index()
        {
            var readings = await _context.WeatherReadings
                .Include(r => r.WeatherStation)
                .ToListAsync();
            return View(readings);
        }
        public async Task<IActionResult> ImportFromApi()
        {
            await _weatherApiService.ImportWeatherData();
            return RedirectToAction(nameof(Index));
        }

        // GET: WeatherReadings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherReading = await _context.WeatherReadings
                .Include(w => w.WeatherStation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherReading == null)
            {
                return NotFound();
            }

            return View(weatherReading);
        }

        // GET: WeatherReadings/Create
        public IActionResult Create()
        {
            ViewData["WeatherStationId"] = new SelectList(
                _context.WeatherStations, "Id", "Name");
            return View();
        }

        // POST: WeatherReadings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Temperature,WindSpeed,Humidity,RecordedAt,WeatherStationId")] WeatherReading weatherReading)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weatherReading);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeatherStationId"] = new SelectList(_context.WeatherStations, "Id", "Location", weatherReading.WeatherStationId);
            return View(weatherReading);
        }

        // GET: WeatherReadings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherReading = await _context.WeatherReadings.FindAsync(id);
            if (weatherReading == null)
            {
                return NotFound();
            }
            ViewData["WeatherStationId"] = new SelectList(_context.WeatherStations, "Id", "Location", weatherReading.WeatherStationId);
            return View(weatherReading);
        }

        // POST: WeatherReadings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Temperature,WindSpeed,Humidity,RecordedAt,WeatherStationId")] WeatherReading weatherReading)
        {
            if (id != weatherReading.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherReading);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherReadingExists(weatherReading.Id))
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
            ViewData["WeatherStationId"] = new SelectList(_context.WeatherStations, "Id", "Location", weatherReading.WeatherStationId);
            return View(weatherReading);
        }

        // GET: WeatherReadings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherReading = await _context.WeatherReadings
                .Include(w => w.WeatherStation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherReading == null)
            {
                return NotFound();
            }

            return View(weatherReading);
        }

        // POST: WeatherReadings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weatherReading = await _context.WeatherReadings.FindAsync(id);
            if (weatherReading != null)
            {
                _context.WeatherReadings.Remove(weatherReading);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherReadingExists(int id)
        {
            return _context.WeatherReadings.Any(e => e.Id == id);
        }
    }
}
