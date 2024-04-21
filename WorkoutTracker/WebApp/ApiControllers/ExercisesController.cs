using System.Net;
using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Exercise controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly ExerciseMapper _exerciseMapper;

        /// <summary>
        /// Exercise controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public ExercisesController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _exerciseMapper = new ExerciseMapper(autoMapper);
        }

        /// <summary>
        /// Get all exercises
        /// </summary>
        /// <returns>List of exercises</returns>
        // GET: api/Exercises
        [ProducesResponseType(typeof(IEnumerable<Exercise>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.Exercise>>> GetExercises()
        {
            return (await _appBll.ExerciseService.AllAsync())
                .Select(e => _exerciseMapper.Map(e)!).ToList();
        }

        /// <summary>
        /// Get exercise by it's id
        /// </summary>
        /// <param name="id">Exercise id</param>
        /// <returns>Exercise or Not Found</returns>
        [ProducesResponseType(typeof(Exercise), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.Exercise>> GetExercise(Guid id)
        {
            var exercise = await _appBll.ExerciseService.FindAsync(id);

            if (exercise == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No exercise found"
                });
            }

            return _exerciseMapper.Map(exercise)!;
        }

        /// <summary>
        /// Edit exercise
        /// </summary>
        /// <param name="id">Exercise to be edited id</param>
        /// <param name="exercise">Updated exercise</param>
        /// <returns>No content - status code 204</returns>
        // PUT: api/Exercises/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExercise(Guid id,
            App.Public.DTO.v1.Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Exercise doesn't match the exercise you want to change"
                });
            }

            var data = _exerciseMapper.Map(exercise);

            if (data == null)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Exercise can not be updated"
                });
            }

            _appBll.ExerciseService.Update(data);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add exercise with the muscle groups being trained
        /// </summary>
        /// <param name="exercise">Exercise with muscle groups</param>
        /// <returns>Created exercise with id, name and description</returns>
        // POST: api/Exercises
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exercise), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.Exercise>> PostExercise(
            App.Public.DTO.v1.CreateExercise exercise)
        {
            if (exercise.MuscleGroupIds.Count == 0)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Can't create exercise without muscle group(s)"
                });
            }

            var newExercise = _appBll.ExerciseService.Add(_exerciseMapper.CreateMapToBll(exercise));

            _appBll.ExerciseMuscleService.AddExerciseMuscles(exercise, newExercise.Id);

            await _appBll.SaveChangesAsync();

            exercise.Id = newExercise.Id;

            return CreatedAtAction("GetExercise", new {id = exercise.Id}, newExercise);
        }

        /// <summary>
        /// Delete exercise
        /// </summary>
        /// <param name="id">Exercise id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/Exercises/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(Guid id)
        {
            var exercise = await _appBll.ExerciseService.FindAsync(id);

            if (exercise == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No exercise found"
                });
            }

            await _appBll.ExerciseService.RemoveAsync(exercise.Id);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}