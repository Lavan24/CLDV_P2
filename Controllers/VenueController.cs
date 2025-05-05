using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEaseApplication.Models;
using EventEaseApplication.Services; // ✅ Make sure this matches your BlobStorageServices namespace

namespace EventEaseApplication.Controllers
{
    public class VenueController : Controller
    {
        private readonly EventEaseDataBaseContext _context;
        private readonly BlobStorageServices _blobStorageService; // ✅ Added Blob storage service

        public VenueController(EventEaseDataBaseContext context, BlobStorageServices blobStorageService)
        {
            _context = context;
            _blobStorageService = blobStorageService; // ✅ Initialize blob storage service
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,VenueName,VenueLocation,VenueCapacity")] VenueEventEase venueEventEase, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var containerClient = await _blobStorageService.GetContainerAsync();
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var blobClient = containerClient.GetBlobClient(fileName);

                    using var stream = imageFile.OpenReadStream();
                    await blobClient.UploadAsync(stream, overwrite: true);

                    venueEventEase.ImageUrl = blobClient.Uri.ToString(); // ✅ Save image URL
                }

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
