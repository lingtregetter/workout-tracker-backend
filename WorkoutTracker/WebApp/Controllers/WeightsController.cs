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
    public class WeightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public WeightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Weights
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Weights.Include(w => w.WorkoutSet);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Weights/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Weights == null)
            {
                return NotFound();
            }

            var weight = await _context.Weights
                .Include(w => w.WorkoutSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weight == null)
            {
                return NotFound();
            }

            return View(weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Weights/Create
        public IActionResult Create()
        {
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        // POST: Weights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsedWeight,WorkoutSetId,Id")] Weight weight)
        {
            if (ModelState.IsValid)
            {
                weight.Id = Guid.NewGuid();
                _context.Add(weight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id", weight.WorkoutSetId);
            return View(weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Weights/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Weights == null)
            {
                return NotFound();
            }

            var weight = await _context.Weights.FindAsync(id);
            if (weight == null)
            {
                return NotFound();
            }
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id", weight.WorkoutSetId);
            return View(weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        // POST: Weights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UsedWeight,WorkoutSetId,Id")] Weight weight)
        {
            if (id != weight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeightExists(weight.Id))
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
            ViewData["WorkoutSetId"] = new SelectList(_context.WorkoutSets, "Id", "Id", weight.WorkoutSetId);
            return View(weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Weights/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Weights == null)
            {
                return NotFound();
            }

            var weight = await _context.Weights
                .Include(w => w.WorkoutSet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weight == null)
            {
                return NotFound();
            }

            return View(weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Weights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Weights == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Weights'  is null.");
            }
            var weight = await _context.Weights.FindAsync(id);
            if (weight != null)
            {
                _context.Weights.Remove(weight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeightExists(Guid id)
        {
          return (_context.Weights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
