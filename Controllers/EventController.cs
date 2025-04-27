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
    public class EventController : Controller
    {
        private readonly EventEaseDataBaseContext _context;

        public EventController(EventEaseDataBaseContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventEventEases.ToListAsync());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEventEase = await _context.EventEventEases
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventEventEase == null)
            {
                return NotFound();
            }

            return View(eventEventEase);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDate,EventDescription,Event_Time")] EventEventEase eventEventEase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventEventEase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventEventEase);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEventEase = await _context.EventEventEases.FindAsync(id);
            if (eventEventEase == null)
            {
                return NotFound();
            }
            return View(eventEventEase);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDate,EventDescription,Event_Time")] EventEventEase eventEventEase)
        {
            if (id != eventEventEase.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventEventEase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventEventEaseExists(eventEventEase.EventId))
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
            return View(eventEventEase);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventEventEase = await _context.EventEventEases
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (eventEventEase == null)
            {
                return NotFound();
            }

            return View(eventEventEase);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventEventEase = await _context.EventEventEases.FindAsync(id);
            if (eventEventEase != null)
            {
                _context.EventEventEases.Remove(eventEventEase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventEventEaseExists(int id)
        {
            return _context.EventEventEases.Any(e => e.EventId == id);
        }
    }
}
