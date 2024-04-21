using App.DAL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Domain;
using App.Domain.Identity;
using Base.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class TrainingProgramsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUnitOfWork _appUnitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="appUnitOfWork"></param>
        public TrainingProgramsController(UserManager<AppUser> userManager, IAppUnitOfWork appUnitOfWork)
        {
            _userManager = userManager;
            _appUnitOfWork = appUnitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: TrainingPrograms
        public async Task<IActionResult> Index()
        {
            var viewModel = await _appUnitOfWork.TrainingProgramRepository.AllAsync();
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: TrainingPrograms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _appUnitOfWork.TrainingProgramRepository.FindAsync(id.Value);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: TrainingPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingProgram"></param>
        /// <returns></returns>
        // POST: TrainingPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainingProgram trainingProgram)
        {
            if (ModelState.IsValid)
            {
                _appUnitOfWork.TrainingProgramRepository.Add(trainingProgram);
                await _appUnitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(trainingProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: TrainingPrograms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _appUnitOfWork.TrainingProgramRepository.FindAsync(id.Value);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trainingProgram"></param>
        /// <returns></returns>
        // POST: TrainingPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TrainingProgram trainingProgram)
        {
            if (id != trainingProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _appUnitOfWork.TrainingProgramRepository.Update(trainingProgram);
                await _appUnitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(trainingProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: TrainingPrograms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingProgram = await _appUnitOfWork.TrainingProgramRepository.FindAsync(id.Value);
            if (trainingProgram == null)
            {
                return NotFound();
            }

            return View(trainingProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: TrainingPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appUnitOfWork.TrainingProgramRepository.RemoveAsync(id);
            await _appUnitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}