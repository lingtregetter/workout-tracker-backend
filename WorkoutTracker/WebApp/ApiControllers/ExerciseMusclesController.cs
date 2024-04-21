using App.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;
using App.Public.DTO.Mappers;
using App.Public.DTO.v1;
using Asp.Versioning;
using AutoMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// Exercise muscle controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ExerciseMusclesController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly ExerciseMuscleMapper _exerciseMuscleMapper;

        /// <summary>
        /// Exercise muscle controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public ExerciseMusclesController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _exerciseMuscleMapper = new ExerciseMuscleMapper(autoMapper);
        }

        /// <summary>
        /// Get all muscle groups with exercises
        /// </summary>
        /// <returns>All muscle groups with specific muscle group exercises</returns>
        // GET: api/ExerciseMuscles
        [ProducesResponseType(typeof(IEnumerable<ExerciseMuscle>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.ExerciseMuscle>>> GetExerciseMuscles()
        {
            return _exerciseMuscleMapper
                .MapToPublicList((await _appBll.ExerciseMuscleService.AllAsync()).ToList());
        }
    }
}