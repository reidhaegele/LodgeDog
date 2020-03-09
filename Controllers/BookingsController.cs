using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LodgeDogDB.Context;
using LodgeDogDB.Models;
using System.Collections;

namespace LodgeDogDB.Controllers
{
    public class BookingsController : Controller
    {
        private readonly mySampleDatabaseContext _context;

        public BookingsController(mySampleDatabaseContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(int id, int month, string inout)
        {
            var mySampleDatabaseContext = _context.Bookings.Include(b => b.NumberNavigation).Where(b => b.Number == id);
            var notes = _context.Notes.Include(b => b.NumberNavigation).Where(b => b.Number == id).ToList<Notes>();
            Owners owner = _context.Owners.Find(id);

            ViewBag.Notes = notes;
            ViewData["Owner"] = id;
            ViewData["Title"] = owner.Firstname + " " + owner.Lastname;

            if (inout == null)
            {

            }
            else
            {
                string m;
                switch (month)
                {
                    case 1:
                        m = "January";
                        break;
                    case 2:
                        m = "February";
                        break;
                    case 3:
                        m = "March";
                        break;
                    case 4:
                        m = "April";
                        break;
                    case 5:
                        m = "May";
                        break;
                    case 6:
                        m = "June";
                        break;
                    case 7:
                        m = "July";
                        break;
                    case 8:
                        m = "August";
                        break;
                    case 9:
                        m = "September";
                        break;
                    case 10:
                        m = "October";
                        break;
                    case 11:
                        m = "November";
                        break;
                    case 12:
                        m = "December";
                        break;
                    default:
                        m = "January";
                        break;
                }
                ViewData["m"] = m;
                ViewData["month"] = month;
                if (inout.Equals("in"))
                {
                    ViewData["inout"] = "in";
                    mySampleDatabaseContext = mySampleDatabaseContext.Where(b => b.Checkin.Month == month);
                }
                else if (inout.Equals("out"))
                {
                    ViewData["inout"] = "out";
                    mySampleDatabaseContext = mySampleDatabaseContext.Where(b => b.Checkout.Month == month);
                }
            }
            double sum = 0.0;
            foreach (Bookings booking in mySampleDatabaseContext)
            {
                double ppt = (double)(booking.Pointsused) / 1000.0;
                sum += (double)(booking.Baserateofpay * ppt);
            }
            ViewData["sum"] = sum;
            return View(await mySampleDatabaseContext.ToListAsync());
        }

        public async Task<IActionResult> Printer(int id, int month, string inout, string paragraph)
        {
            var mySampleDatabaseContext = _context.Bookings.Include(b => b.NumberNavigation).Where(b => b.Number == id);            
            Owners owner = _context.Owners.Find(id);
            
            ViewData["Current"] = DateTime.Today.ToString("D");
            ViewData["Owner"] = id;
            ViewData["Paragraph"] = paragraph;
            ViewData["Name"] = owner.Firstname + " " + owner.Lastname;
            ViewData["fName"] = owner.Firstname;
            ViewData["Address"] = owner.Address;
            ViewData["CSZ"] = owner.City + ", " + owner.State + " " + owner.Zip;

            if (inout == null)
            {

            }
            else
            {
                string m;
                switch (month)
                {
                    case 1:
                        m = "January";
                        break;
                    case 2:
                        m = "February";
                        break;
                    case 3:
                        m = "March";
                        break;
                    case 4:
                        m = "April";
                        break;
                    case 5:
                        m = "May";
                        break;
                    case 6:
                        m = "June";
                        break;
                    case 7:
                        m = "July";
                        break;
                    case 8:
                        m = "August";
                        break;
                    case 9:
                        m = "September";
                        break;
                    case 10:
                        m = "October";
                        break;
                    case 11:
                        m = "November";
                        break;
                    case 12:
                        m = "December";
                        break;
                    default:
                        m = "January";
                        break;
                }
                ViewData["month"] = m;
                if (inout.Equals("in"))
                {
                    mySampleDatabaseContext = mySampleDatabaseContext.Where(b => b.Checkin.Month == month);
                    mySampleDatabaseContext = mySampleDatabaseContext.Where(b => b.Pointsused > 0);
                }
                else if (inout.Equals("out"))
                {
                    mySampleDatabaseContext = mySampleDatabaseContext.Where(b => b.Checkout.Month == month);
                    mySampleDatabaseContext = mySampleDatabaseContext.Where(b => b.Pointsused > 0);
                }
            }
            return View(await mySampleDatabaseContext.ToListAsync());
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStamp,Source,Primaryguestname,Numberofoccupants,Property,Unittype,Datebookingmade,Checkin,Checkout,Number,Baserateofpay,Rci,Tri,Pointsused,Reservationpassesneeded,Reservationpassespurchased,Guestpassesadded,Guestpassespurchased,Wyndhamconfirmationnumber")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number", bookings.Number);
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number", bookings.Number);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("TimeStamp,Source,Primaryguestname,Numberofoccupants,Property,Unittype,Datebookingmade,Checkin,Checkout,Number,Baserateofpay,Rci,Tri,Pointsused,Reservationpassesneeded,Reservationpassespurchased,Guestpassesadded,Guestpassespurchased,Wyndhamconfirmationnumber")] Bookings bookings)
        {
            if (id != bookings.TimeStamp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.TimeStamp))
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
            ViewData["Number"] = new SelectList(_context.Owners, "Number", "Number", bookings.Number);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.NumberNavigation)
                .FirstOrDefaultAsync(m => m.TimeStamp == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(bookings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(DateTime id)
        {
            return _context.Bookings.Any(e => e.TimeStamp == id);
        }
    }
}
