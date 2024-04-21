using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class RepsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Reps
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reps.Include(r => r.WorkoutSet);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Reps/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Reps == null)
            {
                return NotFound();
            }

            var rep = await _context.Reps
                .Include(r => r.WorkoutSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rep == null)
            {
                return NotFound();
            }

            return View(rep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Reps/Create
        public IActionResult Create()
        {
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rep"></param>
        /// <returns></returns>
        // POST: Reps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepAmount,WorkoutSetId,Id")] Rep rep)
        {
            if (ModelState.IsValid)
            {
                rep.Id = Guid.NewGuid();
                _context.Add(rep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id", rep.WorkoutSetId);
            return View(rep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Reps/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Reps == null)
            {
                return NotFound();
            }

            var rep = await _context.Reps.FindAsync(id);
            if (rep == null)
            {
                return NotFound();
            }
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id", rep.WorkoutSetId);
            return View(rep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rep"></param>
        /// <returns></returns>
        // POST: Reps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RepAmount,WorkoutSetId,Id")] Rep rep)
        {
            if (id != rep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepExists(rep.Id))
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
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id", rep.WorkoutSetId);
            return View(rep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Reps/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Reps == null)
            {
                return NotFound();
            }

            var rep = await _context.Reps
                .Include(r => r.WorkoutSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rep == null)
            {
                return NotFound();
            }

            return View(rep);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Reps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Reps == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reps'  is null.");
            }
            var rep = await _context.Reps.FindAsync(id);
            if (rep != null)
            {
                _context.Reps.Remove(rep);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepExists(Guid id)
        {
          return (_context.Reps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
