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
    public class ExerciseMusclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ExerciseMusclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: ExerciseMuscles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExerciseMuscles.Include(e => e.Exercise).Include(e => e.MuscleGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ExerciseMuscles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ExerciseMuscles == null)
            {
                return NotFound();
            }

            var exerciseMuscle = await _context.ExerciseMuscles
                .Include(e => e.Exercise)
                .Include(e => e.MuscleGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseMuscle == null)
            {
                return NotFound();
            }

            return View(exerciseMuscle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: ExerciseMuscles/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName");
            ViewData["MuscleGroupId"] = new SelectList(_context.MuscleGroups, "Id", "MuscleName");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exerciseMuscle"></param>
        /// <returns></returns>
        // POST: ExerciseMuscles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,MuscleGroupId,Id")] ExerciseMuscle exerciseMuscle)
        {
            if (ModelState.IsValid)
            {
                exerciseMuscle.Id = Guid.NewGuid();
                _context.Add(exerciseMuscle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName", exerciseMuscle.ExerciseId);
            ViewData["MuscleGroupId"] = new SelectList(_context.MuscleGroups, "Id", "MuscleName", exerciseMuscle.MuscleGroupId);
            return View(exerciseMuscle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ExerciseMuscles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ExerciseMuscles == null)
            {
                return NotFound();
            }

            var exerciseMuscle = await _context.ExerciseMuscles.FindAsync(id);
            if (exerciseMuscle == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName", exerciseMuscle.ExerciseId);
            ViewData["MuscleGroupId"] = new SelectList(_context.MuscleGroups, "Id", "MuscleName", exerciseMuscle.MuscleGroupId);
            return View(exerciseMuscle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="exerciseMuscle"></param>
        /// <returns></returns>
        // POST: ExerciseMuscles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ExerciseId,MuscleGroupId,Id")] ExerciseMuscle exerciseMuscle)
        {
            if (id != exerciseMuscle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseMuscle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseMuscleExists(exerciseMuscle.Id))
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
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "ExerciseName", exerciseMuscle.ExerciseId);
            ViewData["MuscleGroupId"] = new SelectList(_context.MuscleGroups, "Id", "MuscleName", exerciseMuscle.MuscleGroupId);
            return View(exerciseMuscle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ExerciseMuscles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ExerciseMuscles == null)
            {
                return NotFound();
            }

            var exerciseMuscle = await _context.ExerciseMuscles
                .Include(e => e.Exercise)
                .Include(e => e.MuscleGroup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exerciseMuscle == null)
            {
                return NotFound();
            }

            return View(exerciseMuscle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: ExerciseMuscles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ExerciseMuscles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ExerciseMuscles'  is null.");
            }
            var exerciseMuscle = await _context.ExerciseMuscles.FindAsync(id);
            if (exerciseMuscle != null)
            {
                _context.ExerciseMuscles.Remove(exerciseMuscle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseMuscleExists(Guid id)
        {
          return (_context.ExerciseMuscles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
