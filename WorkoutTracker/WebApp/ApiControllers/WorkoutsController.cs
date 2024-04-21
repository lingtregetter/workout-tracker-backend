using System.Net;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;
using Base.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Workout controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutsController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly WorkoutMapper _workoutMapper;

        /// <summary>
        /// Workout controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public WorkoutsController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _workoutMapper = new WorkoutMapper(autoMapper);
        }

        /// <summary>
        /// Add workout with exercises
        /// </summary>
        /// <param name="workout">Workout with list of exercises</param>
        /// <returns>Ok - status code 200</returns>
        // POST: api/Workouts
        [ProducesResponseType(typeof(Workout),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Workout>> PostWorkout(App.Public.DTO.v1.CreateWorkout workout)
        {
            if (workout.ExerciseIds.Count == 0)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Can't create workout without exercises"
                });
            }

            var newWorkout = _appBll.WorkoutService.Add(_workoutMapper.CreateMapToBll(workout, User.GetUserId()));
            
            await _appBll.SaveChangesAsync();

            workout.Id = newWorkout.Id;

            return Ok();
        }
        
        /// <summary>
        /// Delete workout
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/Workouts/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(Guid id)
        {
            var workout = await _appBll.WorkoutService.FindAsync(id);
           
            if (workout == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No workout found"
                });
            }

            if (!await _appBll.WorkoutService
                    .IsOwnedByUserAsync(workout.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            await _appBll.WorkoutService.RemoveAsync(workout.Id);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}