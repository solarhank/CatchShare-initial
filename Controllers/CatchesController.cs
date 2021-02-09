using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CatchShare.Data;
using CatchShare.Models;
using Microsoft.AspNetCore.Authorization;

namespace CatchShare.Controllers
{
    public class CatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Catch.ToListAsync());
        }
        // GET: Catches/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Catches/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Catch.Where( j => j.Species.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Catches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @catch = await _context.Catch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@catch == null)
            {
                return NotFound();
            }

            return View(@catch);
        }

        // GET: Catches/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Species,Length,Town,State")] Catch @catch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@catch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@catch);
        }

        // GET: Catches/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @catch = await _context.Catch.FindAsync(id);
            if (@catch == null)
            {
                return NotFound();
            }
            return View(@catch);
        }

        // POST: Catches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Species,Length,Town,State")] Catch @catch)
        {
            if (id != @catch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@catch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatchExists(@catch.Id))
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
            return View(@catch);
        }

        // GET: Catches/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @catch = await _context.Catch
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@catch == null)
            {
                return NotFound();
            }

            return View(@catch);
        }

        // POST: Catches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @catch = await _context.Catch.FindAsync(id);
            _context.Catch.Remove(@catch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatchExists(int id)
        {
            return _context.Catch.Any(e => e.Id == id);
        }
    }
}
