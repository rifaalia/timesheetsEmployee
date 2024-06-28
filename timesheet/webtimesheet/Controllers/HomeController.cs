using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webtimesheet.DAL.Repositories;
using webtimesheet.Models;

namespace webtimesheet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TimesheetsDbContext _context;

        public HomeController(ILogger<HomeController> logger, TimesheetsDbContext context)
        {
            _logger = logger;
            _context - context;
        }


        // GET: HomeController
        public async Task <IActionResult> Index()
        {
            return View(await _context.Timesheets.ToListAsync());
        }

        // GET: HomeController/Details/5
        public async Task <IActionResult> Details(int id)
        {
            var timesheet = await _context.Timesheets.FirstOrDefaultAsync(m => m.Id == id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return View(timesheet);

        }


        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timesheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timesheet);
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: HomeController/Edit/5
        public async Task <IActionResult> Edit(int id)
        {
            var timesheet = await _context.Timesheets.FindAsync(id);
            if (timesheet == null)
            {
                return NotFound();
            }
            return View(timesheet);
            //return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(int id, Timesheet timesheet)
        {
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
            if (id != timesheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timesheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimesheetExists(timesheet.Id))
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
            return View(timesheet);
        }

        // GET: HomeController/Delete/5
        public async Task <IActionResult> Delete(int id)
        {
            var timesheet = await _context.Timesheets.FirstOrDefaultAsync(m => m.Id == id);
            if (timesheet == null)
            {
                return NotFound();
            }

            return View(timesheet);
        }

        // POST: HomeController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
