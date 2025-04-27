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
    public class BookingController : Controller
    {
        private readonly EventEaseDataBaseContext _context;

        public BookingController(EventEaseDataBaseContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index()
        {
            var eventEaseDataBaseContext = _context.BookingEventEases.Include(b => b.Event).Include(b => b.Venue);
            return View(await eventEaseDataBaseContext.ToListAsync());
        }

        // GET: Booking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingEventEase = await _context.BookingEventEases
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookingEventEase == null)
            {
                return NotFound();
            }

            return View(bookingEventEase);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.EventEventEases, "EventId", "EventName");
            ViewData["VenueId"] = new SelectList(_context.VenueEventEases, "VenueId", "VenueName");
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,VenueId,EventId,BookingDate,Booking_Time")] BookingEventEase bookingEventEase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingEventEase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.EventEventEases, "EventId", "EventName", bookingEventEase.EventId);
            ViewData["VenueId"] = new SelectList(_context.VenueEventEases, "VenueId", "VenueName", bookingEventEase.VenueId);
            return View(bookingEventEase);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingEventEase = await _context.BookingEventEases.FindAsync(id);
            if (bookingEventEase == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.EventEventEases, "EventId", "EventName", bookingEventEase.EventId);
            ViewData["VenueId"] = new SelectList(_context.VenueEventEases, "VenueId", "VenueName", bookingEventEase.VenueId);
            return View(bookingEventEase);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,VenueId,EventId,BookingDate,Booking_Time")] BookingEventEase bookingEventEase)
        {
            if (id != bookingEventEase.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingEventEase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingEventEaseExists(bookingEventEase.BookingId))
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
            ViewData["EventId"] = new SelectList(_context.EventEventEases, "EventId", "EventName", bookingEventEase.EventId);
            ViewData["VenueId"] = new SelectList(_context.VenueEventEases, "VenueId", "VenueName", bookingEventEase.VenueId);
            return View(bookingEventEase);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingEventEase = await _context.BookingEventEases
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookingEventEase == null)
            {
                return NotFound();
            }

            return View(bookingEventEase);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingEventEase = await _context.BookingEventEases.FindAsync(id);
            if (bookingEventEase != null)
            {
                _context.BookingEventEases.Remove(bookingEventEase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingEventEaseExists(int id)
        {
            return _context.BookingEventEases.Any(e => e.BookingId == id);
        }
    }
}
