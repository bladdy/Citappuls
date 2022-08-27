using Citappuls.Data;
using Citappuls.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Citappuls.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SpecialitiesController : Controller
    {
        private readonly DataContext _context;

        public SpecialitiesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.Countries != null ?
                        View(await _context.Specialties.ToListAsync()) :
                        Problem("Entity set 'DataContext.Countries'  is null.");
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speciality specialty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialty);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(specialty);

        }
        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialties == null)
            {
                return NotFound();
            }

            var country = await _context.Specialties.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Speciality speciality)
        {
            if (id != speciality.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speciality);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un país con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(speciality);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialties == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }
        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialties == null)
            {
                return NotFound();
            }

            var speciality = await _context.Specialties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speciality == null)
            {
                return NotFound();
            }

            return View(speciality);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialties == null)
            {
                return Problem("Entity set 'DataContext.Countries'  is null.");
            }
            var speciality = await _context.Specialties.FindAsync(id);
            if (speciality != null)
            {
                _context.Specialties.Remove(speciality);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
