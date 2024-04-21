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
{   /// <summary>
    /// 
    /// </summary>
    public class WorkoutSetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public WorkoutSetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: WorkoutSets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkoutSets.Include(w => w.WorkoutExercise);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: WorkoutSets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.WorkoutSets == null)
            {
                return NotFound();
            }

            var workoutSet = await _context.WorkoutSets
                .Include(w => w.WorkoutExercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutSet == null)
            {
                return NotFound();
            }

            return View(workoutSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: WorkoutSets/Create
        public IActionResult Create()
        {
            ViewData["WorkoutExerciseId"] = new SelectList(_context.WorkoutExercises, "Id", "Id");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workoutSet"></param>
        /// <returns></returns>
        // POST: WorkoutSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CreatedAt,WorkoutExerciseId,Id")] WorkoutSet workoutSet)
        {
            if (ModelState.IsValid)
            {
                workoutSet.Id = Guid.NewGuid();
                _context.Add(workoutSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkoutExerciseId"] = new SelectList(_context.WorkoutExercises, "Id", "Id", workoutSet.WorkoutExerciseId);
            return View(workoutSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: WorkoutSets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.WorkoutSets == null)
            {
                return NotFound();
            }

            var workoutSet = await _context.WorkoutSets.FindAsync(id);
            if (workoutSet == null)
            {
                return NotFound();
            }
            ViewData["WorkoutExerciseId"] = new SelectList(_context.WorkoutExercises, "Id", "Id", workoutSet.WorkoutExerciseId);
            return View(workoutSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workoutSet"></param>
        /// <returns></returns>
        // POST: WorkoutSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CreatedAt,WorkoutExerciseId,Id")] WorkoutSet workoutSet)
        {
            if (id != workoutSet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutSetExists(workoutSet.Id))
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
            ViewData["WorkoutExerciseId"] = new SelectList(_context.WorkoutExercises, "Id", "Id", workoutSet.WorkoutExerciseId);
            return View(workoutSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: WorkoutSets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.WorkoutSets == null)
            {
                return NotFound();
            }

            var workoutSet = await _context.WorkoutSets
                .Include(w => w.WorkoutExercise)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutSet == null)
            {
                return NotFound();
            }

            return View(workoutSet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: WorkoutSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.WorkoutSets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WorkoutSets'  is null.");
            }
            var workoutSet = await _context.WorkoutSets.FindAsync(id);
            if (workoutSet != null)
            {
                _context.WorkoutSets.Remove(workoutSet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutSetExists(Guid id)
        {
          return (_context.WorkoutSets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
