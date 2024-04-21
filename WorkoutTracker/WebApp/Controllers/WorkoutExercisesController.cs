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
    public class WorkoutExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public WorkoutExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: WorkoutExercises
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkoutExercises.Include(w => w.Exercise).Include(w => w.Workout);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: WorkoutExercises/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.WorkoutExercises == null)
            {
                return NotFound();
            }

            var workoutExercise = await _context.WorkoutExercises
                .Include(w => w.Exercise)
                .Include(w => w.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutExercise == null)
            {
                return NotFound();
            }

            return View(workoutExercise);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: WorkoutExercises/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName");
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "WorkoutName");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workoutExercise"></param>
        /// <returns></returns>
        // POST: WorkoutExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Notes,WorkoutId,ExerciseId,Id")] WorkoutExercise workoutExercise)
        {
            if (ModelState.IsValid)
            {
                workoutExercise.Id = Guid.NewGuid();
                _context.Add(workoutExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName", workoutExercise.ExerciseId);
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "WorkoutName", workoutExercise.WorkoutId);
            return View(workoutExercise);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: WorkoutExercises/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.WorkoutExercises == null)
            {
                return NotFound();
            }

            var workoutExercise = await _context.WorkoutExercises.FindAsync(id);
            if (workoutExercise == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName", workoutExercise.ExerciseId);
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "WorkoutName", workoutExercise.WorkoutId);
            return View(workoutExercise);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="workoutExercise"></param>
        /// <returns></returns>
        // POST: WorkoutExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Notes,WorkoutId,ExerciseId,Id")] WorkoutExercise workoutExercise)
        {
            if (id != workoutExercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutExercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExerciseExists(workoutExercise.Id))
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
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName", workoutExercise.ExerciseId);
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "WorkoutName", workoutExercise.WorkoutId);
            return View(workoutExercise);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: WorkoutExercises/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.WorkoutExercises == null)
            {
                return NotFound();
            }

            var workoutExercise = await _context.WorkoutExercises
                .Include(w => w.Exercise)
                .Include(w => w.Workout)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutExercise == null)
            {
                return NotFound();
            }

            return View(workoutExercise);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: WorkoutExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.WorkoutExercises == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WorkoutExercises'  is null.");
            }
            var workoutExercise = await _context.WorkoutExercises.FindAsync(id);
            if (workoutExercise != null)
            {
                _context.WorkoutExercises.Remove(workoutExercise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExerciseExists(Guid id)
        {
          return (_context.WorkoutExercises?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
