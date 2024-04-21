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
    /// Training program controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainingProgramsController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly TrainingProgramMapper _trainingProgramMapper;

        /// <summary>
        /// Training program controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public TrainingProgramsController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _trainingProgramMapper = new TrainingProgramMapper(autoMapper);
        }

        /// <summary>
        /// Get training program with training blocks by program id
        /// </summary>
        /// <param name="id">Training program id</param>
        /// <returns>Training program with training blocks</returns>
        // GET: api/TrainingPrograms/5
        [ProducesResponseType(typeof(TrainingProgram), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.TrainingProgram>> GetTrainingProgram(Guid id)
        {
            var trainingProgram = await _appBll.TrainingProgramService.FindAsync(id);

            if (trainingProgram == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No training program found"
                });
            }

            if (!await _appBll.TrainingProgramService
                    .IsOwnedByUserAsync(trainingProgram.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            return _trainingProgramMapper.MapToPublic(trainingProgram);
        }
        
        /// <summary>
        /// Edit user training program
        /// </summary>
        /// <param name="id">Training program to be edited id</param>
        /// <param name="trainingProgram">Updated training program</param>
        /// <returns>No content - status code 204</returns>
        // PUT: api/TrainingPrograms/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingProgram(Guid id,
            App.Public.DTO.v1.TrainingProgram trainingProgram)
        {
            if (id != trainingProgram.Id)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Training program doesn't match the training program you want to change"
                });
            }

            if (!await _appBll.TrainingProgramService
                    .IsOwnedByUserAsync(trainingProgram.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            var data = _trainingProgramMapper.Map(trainingProgram);
            
            data!.AppUserId = User.GetUserId();
            
            _appBll.TrainingProgramService.Update(data);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create training program with training blocks for user
        /// </summary>
        /// <param name="trainingProgram">Training program info with training blocks</param>
        /// <returns>Created training program</returns>
        // POST: api/TrainingPrograms
        [ProducesResponseType(typeof(TrainingProgram), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.TrainingProgram>> PostTrainingProgram(
            App.Public.DTO.v1.CreateTrainingProgram trainingProgram)
        {
            if (trainingProgram.Blocks.Count == 0)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Can't create training program without training blocks"
                });
            }

            var program = _appBll.TrainingProgramService
                .Add(_trainingProgramMapper.CreateMapToBll(trainingProgram, User.GetUserId()));

            await _appBll.SaveChangesAsync();

            trainingProgram.Id = program.Id;
            
            return CreatedAtAction("GetTrainingProgram", new {id = trainingProgram.Id}, trainingProgram);
        }
        
        /// <summary>
        /// Delete user training program
        /// </summary>
        /// <param name="id">Training program id</param>
        /// <returns>No content - status code 204</returns>
        // DELETE: api/TrainingPrograms/5
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingProgram(Guid id)
        {
            var trainingProgram = await _appBll.TrainingProgramService.FindAsync(id);
           
            if (trainingProgram == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No training program found"
                });
            }

            if (!await _appBll.TrainingProgramService
                    .IsOwnedByUserAsync(trainingProgram.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            await _appBll.TrainingProgramService.RemoveAsync(trainingProgram.Id);
            
            await _appBll.SaveChangesAsync();

            return NoContent();
        }
    }
}