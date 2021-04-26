using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWepApp2.Data;
using HotelWepApp2.Models;
using HotelWepApp2.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace HotelWepApp2.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Jobs.ToListAsync());
        }

        [Authorize("IsReceptionist")]
        public async Task<IActionResult> Reservations()
        {
            return View(await _context.Buffets.Include(b=>b.Guest).ToListAsync());
        }

        [Authorize("IsReceptionist")]
        public async Task<IActionResult> Rooms()
        {
            return View(await _context.Jobs.ToListAsync());
        }

        [Authorize("IsWaiter")]
        public async Task<IActionResult> WaiterIndex()
        {
            return View(await _context.Guests.Include(g => g.Room).ToListAsync());
        }

        [Authorize("IsWaiter")]
        public async Task<IActionResult> GuestCheckIn(long? id)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == id);
            var buffet = _context.Buffets.FirstOrDefault(b => b.Date == DateTime.Today);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                guest.BuffetCheckIn = true;
            }
            
            return View(await _context.SaveChangesAsync());
        }

        [Authorize("IsWaiter")]
        public async Task<IActionResult> GuestCheckOut(long? id)
        {
            var guest = _context.Guests.FirstOrDefault(g => g.GuestId == id);
            var buffet = _context.Buffets.FirstOrDefault(b => b.Date == DateTime.Today);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                guest.BuffetCheckIn = false;
            }

            _context.SaveChanges();
            return View("~/Views/Jobs/GuestCheckIn.cshtml");
        }

        [Authorize("IsChef")]
        public ActionResult KitchenInformation()
        {
            var applicationDbContext = _context.Buffets.Include(b => b.Guest);
            var model = new ChefViewModel();
            foreach (var buffet in applicationDbContext)
            {
                foreach (var guest in buffet.Guest)
                {
                    if (guest.Type == "Adult" && guest.BuffetCheckIn == true)
                    {
                        model.AdultCheckIn++;
                    }
                }
            }
            
            return View(model);
        }


        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GuestId,Date")] ReceptionistViewModel receptionistViewModel)
        {
            
            if (ModelState.IsValid)
            {
                var guest = _context.Guests.Include(g => g.Room).SingleOrDefault(g => g.GuestId == receptionistViewModel.GuestId);
                var buffet = _context.Buffets.SingleOrDefault(b => b.Date == receptionistViewModel.Date);
                if (guest == null)
                {
                    return NotFound();
                }

                if (buffet == null)
                {
                    Buffet newBuffet = new Buffet()
                    {
                        Date = receptionistViewModel.Date,
                        Guest = new List<Guest>()
                    };
                    newBuffet.Guest.Add(guest);
                    _context.Buffets.Add(newBuffet);
                    
                    
                }
                else
                {
                    buffet.Guest = new List<Guest>();
                    buffet.Guest.Add(guest);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Jobs/Reservations.cshtml");
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("JobId,Name,Email")] Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobId))
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
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(long id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
