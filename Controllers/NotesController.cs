using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LodgeDogDB.Context;
using LodgeDogDB.Models;

namespace LodgeDogDB.Controllers
{
    public class NotesController : Controller
    {
        private readonly mySampleDatabaseContext _context;

        public NotesController(mySampleDatabaseContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var mySampleDatabaseContext = _context.Notes.Include(n => n.NumberNavigation);
            return View(await mySampleDatabaseContext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.NumberNavigation)
                .FirstOrDefaultAsync(m => m.TimeStamp == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStamp,Name,Note,Reason,Number")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number", notes.Number);
            return View(notes);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number", notes.Number);
            return View(notes);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("TimeStamp,Name,Note,Reason,Number")] Notes notes)
        {
            if (id != notes.TimeStamp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotesExists(notes.TimeStamp))
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
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number", notes.Number);
            return View(notes);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notes = await _context.Notes
                .Include(n => n.NumberNavigation)
                .FirstOrDefaultAsync(m => m.TimeStamp == id);
            if (notes == null)
            {
                return NotFound();
            }

            return View(notes);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var notes = await _context.Notes.FindAsync(id);
            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotesExists(DateTime id)
        {
            return _context.Notes.Any(e => e.TimeStamp == id);
        }
    }
}
