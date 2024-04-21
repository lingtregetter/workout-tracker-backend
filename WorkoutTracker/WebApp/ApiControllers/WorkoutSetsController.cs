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
    /// Workout set controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WorkoutSetsController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly WorkoutSetMapper _workoutSetMapper;

        /// <summary>
        /// Workout set controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public WorkoutSetsController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _workoutSetMapper = new WorkoutSetMapper(autoMapper);
        }

        /// <summary>
        /// Get all workout exercise workout sets with rep x weight by workout exercise id
        /// </summary>
        /// <param name="id">Workout exercise id</param>
        /// <returns>List of workout sets</returns>
        // GET: api/WorkoutSets/5
        [ProducesResponseType(typeof(List<WorkoutSet>),StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<WorkoutSet>>> GetWorkoutSet(Guid id)
        {
            return _workoutSetMapper.MapToPublicList(await _appBll.WorkoutSetService.AllAsync(id));
        }

        /// <summary>
        /// Add workout set with rep count and used weight
        /// </summary>
        /// <param name="workoutSet">Workout set with rep and weight</param>
        /// <returns>Ok - status code 200</returns>
        // POST: api/WorkoutSets
        [ProducesResponseType(typeof(WorkoutSet), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<WorkoutSet>> PostWorkoutSet(CreateWorkoutSet workoutSet)
        {
            _appBll.WorkoutSetService.Add(_workoutSetMapper.MapToBll(workoutSet));
            await _appBll.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Edit workout set
        /// </summary>
        /// <param name="id">Workout set to be edited id</param>
        /// <param name="workoutSet"></param>
        /// <returns>No content - status code 204</returns>
        // PUT: api/WorkoutSets/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkoutSet(Guid id, WorkoutSet workoutSet)
        {
            if (id != workoutSet.Id)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Workout set doesn't match the workout set you want to change"
                });
            }

            var setFromDb = await _appBll.WorkoutSetService.FindAsync(id);

            if (setFromDb == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No workout set found"
                });
            }

            _appBll.WorkoutSetService.UpdateSet(_workoutSetMapper.MapToBll(workoutSet));

            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Delete workout set
        /// </summary>
        /// <param name="id">Workout set id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/WorkoutSets/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutSets(Guid id)
        {
            var workoutSet = await _appBll.WorkoutSetService.FindAsync(id);

            if (workoutSet == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No workout set found"
                });
            }

            await _appBll.WorkoutSetService.RemoveAsync(workoutSet.Id);

            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}