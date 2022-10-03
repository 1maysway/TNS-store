using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TNS_store2.Models;

namespace TNS_store2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FabricatorsController : Controller
    {
        private readonly TnsDbContext _context;

        public FabricatorsController(TnsDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Fabricators
        public async Task<IActionResult> Index()
        {
              return View(await _context.Fabricators.ToListAsync());
        }

        // GET: Admin/Fabricators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fabricators == null)
            {
                return NotFound();
            }

            var fabricator = await _context.Fabricators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fabricator == null)
            {
                return NotFound();
            }

            return View(fabricator);
        }

        // GET: Admin/Fabricators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Fabricators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl,Country")] Fabricator fabricator)
        {
            //if (ModelState.IsValid)
            //{
                _context.Fabricators.Add(fabricator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(fabricator);
        }

        // GET: Admin/Fabricators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fabricators == null)
            {
                return NotFound();
            }

            var fabricator = await _context.Fabricators.FindAsync(id);
            if (fabricator == null)
            {
                return NotFound();
            }
            return View(fabricator);
        }

        // POST: Admin/Fabricators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,Country")] Fabricator fabricator)
        {
            if (id != fabricator.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(fabricator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FabricatorExists(fabricator.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            return View(fabricator);
        }

        // GET: Admin/Fabricators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fabricators == null)
            {
                return NotFound();
            }

            var fabricator = await _context.Fabricators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fabricator == null)
            {
                return NotFound();
            }

            return View(fabricator);
        }

        // POST: Admin/Fabricators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fabricators == null)
            {
                return Problem("Entity set 'TnsDbContext.Fabricators'  is null.");
            }
            var fabricator = await _context.Fabricators.FindAsync(id);
            if (fabricator != null)
            {
                _context.Fabricators.Remove(fabricator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FabricatorExists(int id)
        {
          return _context.Fabricators.Any(e => e.Id == id);
        }
    }
}
