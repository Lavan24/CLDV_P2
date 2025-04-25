using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventEaseApplication.Models;

namespace EventEaseApplication.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDataBaseContext _context;

        public VenueController(EventEaseDataBaseContext context)
        {
            _context = context;
        }

        // GET: Venue
        public async Task<IActionResult> Index()
        {
            return View(await _context.VenueEventEases.ToListAsync());
        }

        // GET: Venue/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueEventEase = await _context.VenueEventEases
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venueEventEase == null)
            {
                return NotFound();
            }

            return View(venueEventEase);
        }

        // GET: Venue/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Venue/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,VenueName,VenueLocation,VenueCapacity,ImageUrl")] VenueEventEase venueEventEase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venueEventEase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venueEventEase);
        }

        // GET: Venue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueEventEase = await _context.VenueEventEases.FindAsync(id);
            if (venueEventEase == null)
            {
                return NotFound();
            }
            return View(venueEventEase);
        }

        // POST: Venue/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,VenueLocation,VenueCapacity,ImageUrl")] VenueEventEase venueEventEase)
        {
            if (id != venueEventEase.VenueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venueEventEase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueEventEaseExists(venueEventEase.VenueId))
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
            return View(venueEventEase);
        }

        // GET: Venue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueEventEase = await _context.VenueEventEases
                .FirstOrDefaultAsync(m => m.VenueId == id);
            if (venueEventEase == null)
            {
                return NotFound();
            }

            return View(venueEventEase);
        }

        // POST: Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueEventEase = await _context.VenueEventEases.FindAsync(id);
            if (venueEventEase != null)
            {
                _context.VenueEventEases.Remove(venueEventEase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueEventEaseExists(int id)
        {
            return _context.VenueEventEases.Any(e => e.VenueId == id);
        }
    }
}
