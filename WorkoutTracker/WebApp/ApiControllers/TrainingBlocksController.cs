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
    /// Training block controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainingBlocksController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly TrainingBlockMapper _trainingBlockMapper;

        /// <summary>
        /// Training block controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public TrainingBlocksController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _trainingBlockMapper = new TrainingBlockMapper(autoMapper);
        }

        /// <summary>
        /// Get training block with block workouts by block id
        /// </summary>
        /// <param name="id">Training block id</param>
        /// <returns>Training block with block workouts</returns>
        // GET: api/TrainingBlocks/5
        [ProducesResponseType(typeof(TrainingBlock), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.TrainingBlock>> GetTrainingBlock(Guid id)
        {
            var trainingBlock = await _appBll.TrainingBlockService.FindAsync(id);

            if (trainingBlock == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No training block found"
                });
            }

            if (!await _appBll.TrainingBlockService
                    .IsOwnedByUserAsync(trainingBlock.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            return _trainingBlockMapper.MapToPublic(trainingBlock)!;
        }

        /// <summary>
        /// Create training blocks
        /// </summary>
        /// <param name="trainingBlock">Training blocks</param>
        /// <returns>Ok - status code 200</returns>
        [ProducesResponseType(typeof(TrainingBlock), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.TrainingBlock>> PostTrainingBlock(
            App.Public.DTO.v1.TrainingBlockWithProgram trainingBlock)
        {
            var data = _trainingBlockMapper.MapToBll(trainingBlock, User.GetUserId());
            
            if (!await _appBll.TrainingProgramService.IsOwnedByUserAsync(trainingBlock.TrainingProgramId,
                    User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            data.ForEach(block => _appBll.TrainingBlockService.Add(block));

            await _appBll.SaveChangesAsync();

            return Ok();
        }
        
        /// <summary>
        /// Delete training block
        /// </summary>
        /// <param name="id">Training block id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/TrainingBlocks/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingBlock(Guid id)
        {
            var trainingBlock = await _appBll.TrainingBlockService.FindAsync(id);
           
            if (trainingBlock == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No training block found"
                });
            }

            if (!await _appBll.TrainingBlockService
                    .IsOwnedByUserAsync(trainingBlock.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            await _appBll.TrainingBlockService.RemoveAsync(trainingBlock.Id);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}