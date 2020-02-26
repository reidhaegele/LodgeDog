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
    public class ClientFinderController : Controller
    {
        private readonly mySampleDatabaseContext _context;

        public ClientFinderController(mySampleDatabaseContext context)
        {
            _context = context;
        }

        // GET: ClientFinder
        public async Task<IActionResult> Index(int actual)
        {
            List<PointsGroup> groups = new List<PointsGroup>();
            var conn = _context.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    string query = "SELECT Number, Firstname, Lastname, Membernumber, Viplevel, Points, Rateofpay, Guestpasses, Reservationpasses "
                        + "FROM Owners "
                        + "WHERE POINTS > 0 AND GUESTPASSES > 0 "
                        + "GROUP BY Number, Firstname, Lastname, Membernumber, Viplevel, Points, Rateofpay, Guestpasses, Reservationpasses";
                    command.CommandText = query;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            var row = new PointsGroup { Number = reader.GetInt32(0), 
                                Firstname = reader.GetString(1), 
                                Lastname = reader.GetString(2), 
                                Membernumber = reader.GetString(3), 
                                Viplevel = reader.GetString(4),
                                Points = reader.GetInt32(5),
                                Rateofpay = reader.GetDouble(6),
                                Guestpasses = reader.GetInt16(7),
                                Reservationpasses = reader.GetInt16(8),
                                Compatibility = (Math.Abs(((float)reader.GetInt32(5) / (float)reader.GetInt16(7)) - actual) / ((float)reader.GetInt32(5) / (float)reader.GetInt16(7)))*100

                            };
                            Console.WriteLine(row.Firstname + ": " + row.Compatibility);
                            groups.Add(row);
                        }
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            groups = groups.OrderByDescending(g => g.Compatibility).ToList();
            return View(groups);
        }

        // GET: ClientFinder/Details/5
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

        // GET: ClientFinder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientFinder/Create
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

        // GET: ClientFinder/Edit/5
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

        // POST: ClientFinder/Edit/5
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

        // GET: ClientFinder/Delete/5
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

        // POST: ClientFinder/Delete/5
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
    }
}
