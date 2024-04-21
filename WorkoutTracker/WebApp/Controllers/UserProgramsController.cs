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
    public class UserProgramsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUnitOfWork _appUnitOfWork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="appUnitOfWork"></param>
        public UserProgramsController(UserManager<AppUser> userManager, IAppUnitOfWork appUnitOfWork)
        {
            _userManager = userManager;
            _appUnitOfWork = appUnitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: UserPrograms
        public async Task<IActionResult> Index()
        {
            var viewModel = await _appUnitOfWork
                .UserProgramRepository.AllAsync(User.GetUserId());
            return View(viewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserPrograms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProgram = await _appUnitOfWork.UserProgramRepository.FindAsync(id.Value, User.GetUserId());
            if (userProgram == null)
            {
                return NotFound();
            }

            return View(userProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: UserPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProgram"></param>
        /// <returns></returns>
        // POST: UserPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProgram userProgram)
        {
            userProgram.AppUserId = User.GetUserId();

            if (ModelState.IsValid)
            {
                _appUnitOfWork.UserProgramRepository.Add(userProgram);
                await _appUnitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(userProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserPrograms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProgram = await _appUnitOfWork.UserProgramRepository.FindAsync(id.Value, User.GetUserId());
            if (userProgram == null)
            {
                return NotFound();
            }

            return View(userProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProgram"></param>
        /// <returns></returns>
        // POST: UserPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserProgram userProgram)
        {
            // TODO: check the ownership before edit!!!! also before delete

            if (id != userProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && await _appUnitOfWork.UserProgramRepository
                    .IsOwnedByUserAsync(userProgram.Id, User.GetUserId()))
            {
                userProgram.AppUserId = User.GetUserId();

                _appUnitOfWork.UserProgramRepository.Update(userProgram);
                await _appUnitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(userProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserPrograms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProgram = await _appUnitOfWork.UserProgramRepository.FindAsync(id.Value, User.GetUserId());
            if (userProgram == null)
            {
                return NotFound();
            }

            return View(userProgram);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: UserPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _appUnitOfWork.UserProgramRepository.RemoveAsync(id, User.GetUserId());
            await _appUnitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}