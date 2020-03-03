using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LodgeDogDB.Models;
using LodgeDogDB.Context;
using System.Data.Common;

namespace LodgeDogDB.Controllers
{
    public class OwnersController : Controller
    {
        private readonly mySampleDatabaseContext _context;

        public OwnersController(mySampleDatabaseContext context)
        {
            _context = context;
        }

        // GET: Owners
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            var owners = from o in _context.Owners
                       select o;
            if (!String.IsNullOrEmpty(searchString))
            {
                owners = owners.Where(o => o.Firstname.Contains(searchString)
                                       || o.Lastname.Contains(searchString));
            }
            int pageSize = 2;
            return View(await PaginatedList<Owners>.CreateAsync(owners.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owners = await _context.Owners
                .FirstOrDefaultAsync(m => m.Number == id);
            if (owners == null)
            {
                return NotFound();
            }

            return View(owners);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,TimeStamp,Fullservice,Signupdate,Firstname,Lastname,Primarycontact,Phonenumber,Alternatephonenumber,Alternatephonenumber2,Email,Alternateemail,Address,City,State,Zip,Membernumber,Viplevel,Points,Expiration,Yearofuse,Rcimembernumber,Rcipoints,Rciyearofuse,Rateofpay,Username,Password,Rci1,Rci2,Guestpasses,Reservationpasses,Wynresemail")] Owners owners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owners);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owners = await _context.Owners.FindAsync(id);
            if (owners == null)
            {
                return NotFound();
            }
            return View(owners);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Number,TimeStamp,Fullservice,Signupdate,Firstname,Lastname,Primarycontact,Phonenumber,Alternatephonenumber,Alternatephonenumber2,Email,Alternateemail,Address,City,State,Zip,Membernumber,Viplevel,Points,Expiration,Yearofuse,Rcimembernumber,Rcipoints,Rciyearofuse,Rateofpay,Username,Password,Rci1,Rci2,Guestpasses,Reservationpasses,Wynresemail")] Owners owners)
        {
            if (id != owners.Number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnersExists(owners.Number))
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
            return View(owners);
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owners = await _context.Owners
                .FirstOrDefaultAsync(m => m.Number == id);
            if (owners == null)
            {
                return NotFound();
            }

            return View(owners);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owners = await _context.Owners.FindAsync(id);
            _context.Owners.Remove(owners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnersExists(int id)
        {
            return _context.Owners.Any(e => e.Number == id);
        }

        // GET: Owners/Use/5
        public async Task<IActionResult> Use(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owners = await _context.Owners.FindAsync(id);
            if (owners == null)
            {
                return NotFound();
            }
            return View(owners);
        }

        // POST: Owners/Use/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Use(int id, [Bind("Number,TimeStamp,Fullservice,Signupdate,Firstname,Lastname,Primarycontact,Phonenumber,Alternatephonenumber,Alternatephonenumber2,Email,Alternateemail,Address,City,State,Zip,Membernumber,Viplevel,Points,Expiration,Yearofuse,Rcimembernumber,Rcipoints,Rciyearofuse,Rateofpay,Username,Password,Rci1,Rci2,Guestpasses,Reservationpasses,Wynresemail")] Owners owners)
        {
            if (id != owners.Number)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(owners);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OwnersExists(owners.Number))
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
            return View(owners);
        }
    }
}
