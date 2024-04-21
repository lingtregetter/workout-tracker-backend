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
    /// User program controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProgramsController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly UserProgramMapper _userProgramMapper;

        /// <summary>
        /// User program controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public UserProgramsController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _userProgramMapper = new UserProgramMapper(autoMapper);
        }

        /// <summary>
        /// Get all user programs
        /// </summary>
        /// <returns>List of user programs</returns>
        // GET: api/UserPrograms
        [ProducesResponseType(typeof(IEnumerable<UserProgram>),StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App.Public.DTO.v1.UserProgram>>> GetUserPrograms()
        {
            return (await _appBll.UserProgramService.AllAsync(User.GetUserId()))
                .Select(u => _userProgramMapper.MapToPublic(u)!).ToList();
        }
    }
}