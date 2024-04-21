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
    /// Workout exercise controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly WorkoutExerciseMapper _workoutExerciseMapper;

        /// <summary>
        /// Workout exercise controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public WorkoutExercisesController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _workoutExerciseMapper = new WorkoutExerciseMapper(autoMapper);
        }

        /// <summary>
        /// Get workout with exercises by workout id
        /// </summary>
        /// <param name="id">Workout id</param>
        /// <returns>Workout with exercises</returns>
        // GET: api/WorkoutExercises/5
        [ProducesResponseType(typeof(WorkoutExercise), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.WorkoutExercise>> GetWorkoutExercise(Guid id)
        {
            if (!await _appBll.WorkoutService.IsOwnedByUserAsync(id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            var workout = await _appBll.WorkoutExerciseService.FindAsyncByWorkoutId(id);
            if (workout.Count == 0)
            {
                var w = await _appBll.WorkoutService.FindAsync(id);
                return new App.Public.DTO.v1.WorkoutExercise()
                {
                    Id = w!.Id,
                    WorkoutName = w.WorkoutName,
                    Exercises = new List<WorkoutExerciseDetails>()
                };
            }

            return _workoutExerciseMapper.MapToPublic(workout);
        }

        /// <summary>
        /// Add workout exercises
        /// </summary>
        /// <param name="workoutExercise">Workout exercises</param>
        /// <returns>Ok - status code 200</returns>
        [ProducesResponseType(typeof(WorkoutExercise),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.WorkoutExercise>> PostWorkoutExercise(
            App.Public.DTO.v1.WorkoutExerciseWithWorkout workoutExercise)
        {
            if (!await _appBll.WorkoutService.IsOwnedByUserAsync(workoutExercise.WorkoutId, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            _appBll.WorkoutExerciseService.AddWorkoutExercises(workoutExercise);

            await _appBll.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Delete workout exercise
        /// </summary>
        /// <param name="id">Workout exercise id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/WorkoutExercises/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutExercise(Guid id)
        {
            var workoutExercise = await _appBll.WorkoutExerciseService.FindAsync(id);

            if (workoutExercise == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No workout exercise found"
                });
            }

            if (!await _appBll.WorkoutService
                    .IsOwnedByUserAsync(workoutExercise.WorkoutId, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            await _appBll.WorkoutExerciseService.RemoveAsync(workoutExercise.Id);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}