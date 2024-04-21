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
    /// Muscle groups controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class MuscleGroupsController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly MuscleGroupsMapper _muscleGroupsMapper;

        /// <summary>
        /// Muscle group controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public MuscleGroupsController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _muscleGroupsMapper = new MuscleGroupsMapper(autoMapper);
        }

        /// <summary>
        /// Get all muscle groups
        /// </summary>
        /// <returns>List of muscle groups</returns>
        // GET: api/MuscleGroups
        [ProducesResponseType(typeof(IEnumerable<MuscleGroup>),StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.MuscleGroup>>> GetMuscleGroups()
        {
            return (await _appBll.MuscleGroupService.AllAsync())
                .Select(m => _muscleGroupsMapper.MapToPublic(m)).ToList();
        }


        /// <summary>
        /// Get muscle group by it's id
        /// </summary>
        /// <param name="id">Muscle group id</param>
        /// <returns>Muscle group or Not Found</returns>
        [ProducesResponseType(typeof(MuscleGroup),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.MuscleGroup>> GetMuscleGroup(Guid id)
        {
            var muscleGroup = await _appBll.MuscleGroupService.FindAsync(id);

            if (muscleGroup == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No muscle group found"
                });
            }

            return _muscleGroupsMapper.Map(muscleGroup)!;
        }

        /// <summary>
        /// Edit muscle group
        /// </summary>
        /// <param name="id">Muscle group to be edited id</param>
        /// <param name="muscleGroup">Updated muscleGroup</param>
        /// <returns>No content - status code 204</returns>
        // PUT: api/MuscleGroups/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMuscleGroup(Guid id,
            App.Public.DTO.v1.MuscleGroup muscleGroup)
        {
            if (id != muscleGroup.Id)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Muscle group doesn't match the muscle group you want to change"
                });
            }

            var data = _muscleGroupsMapper.Map(muscleGroup);

            if (data == null)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Muscle group can not be updated"
                });
            }

            _appBll.MuscleGroupService.Update(data);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create muscle group
        /// </summary>
        /// <param name="muscleGroup">Muscle groups</param>
        /// <returns>Ok - status code 200</returns>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MuscleGroup),StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.MuscleGroup>> PostMuscleGroup(
            App.Public.DTO.v1.MuscleGroup muscleGroup)
        {
            var data = _muscleGroupsMapper.Map(muscleGroup);

            if (data == null)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Muscle group can not be added"
                });
            }

            _appBll.MuscleGroupService.Add(data);

            await _appBll.SaveChangesAsync();

            return CreatedAtAction("GetMuscleGroup", new {id = data.Id}, muscleGroup);
        }
        
          
        /// <summary>
        /// Delete muscle group
        /// </summary>
        /// <param name="id">Muscle group id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/MuscleGroups/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMuscleGroup(Guid id)
        {
            var muscleGroup = await _appBll.MuscleGroupService.FindAsync(id);
           
            if (muscleGroup == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No muscle group found"
                });
            }

            await _appBll.MuscleGroupService.RemoveAsync(muscleGroup.Id);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}