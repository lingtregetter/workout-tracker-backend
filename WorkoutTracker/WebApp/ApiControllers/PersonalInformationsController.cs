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
    /// Personal information controller class
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonalInformationsController : ControllerBase
    {
        private readonly IAppBLL _appBll;
        private readonly PersonalInformationMapper _personalInformationMapper;

        /// <summary>
        /// Personal information controller constructor
        /// </summary>
        /// <param name="autoMapper">Automapper</param>
        /// <param name="appBll">Provides access to entities</param>
        public PersonalInformationsController(IMapper autoMapper, IAppBLL appBll)
        {
            _appBll = appBll;
            _personalInformationMapper = new PersonalInformationMapper(autoMapper);
        }

        /// <summary>
        /// Get user personal information
        /// </summary>
        /// <returns>User personal information (gender, height, etc)</returns>
        // GET: api/PersonalInformations
        [ProducesResponseType(typeof(PersonalInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<PersonalInformation?>> GetPersonalInformation()
        {
            var personalInformation = await _appBll.PersonalInformationService.FindAsync(User.GetUserId());
            
            if (personalInformation == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No personal information found"
                });
            }

            return _personalInformationMapper.Map(personalInformation!);
        }

        /// <summary>
        /// Get personal information by it's id
        /// </summary>
        /// <param name="id">Personal information id</param>
        /// <returns>Personal information or Not Found</returns>
        [ProducesResponseType(typeof(PersonalInformation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<App.Public.DTO.v1.PersonalInformation>> GetPersonalInformation(Guid id)
        {
            var personalInformation = await _appBll.PersonalInformationService.FindAsync(id, User.GetUserId());

            if (personalInformation == null)
            {
                return NotFound(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.NotFound,
                    Error = "No personal information found"
                });
            }

            return _personalInformationMapper.Map(personalInformation)!;
        }

        /// <summary>
        /// Edit user personal information
        /// </summary>
        /// <param name="id">Personal information to be edited id</param>
        /// <param name="personalInformation">Updated personal information</param>
        /// <returns>No content - status code 204</returns>
        // PUT: api/PersonalInformations/5
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status204NoContent)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalInformation(Guid id,
            App.Public.DTO.v1.PersonalInformation personalInformation)
        {
            if (id != personalInformation.Id)
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "Personal information doesn't match the personal information you want to change"
                });
            }

            if (!await _appBll.PersonalInformationService
                    .IsOwnedByUserAsync(personalInformation.Id, User.GetUserId()))
            {
                return BadRequest(new RestApiErrorResponse()
                {
                    Status = HttpStatusCode.BadRequest,
                    Error = "No hacking (bad user id)!"
                });
            }

            var data = _personalInformationMapper.Map(personalInformation);
            
            data!.AppUserId = User.GetUserId();
            
            _appBll.PersonalInformationService.Update(data);
            await _appBll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add user personal information
        /// </summary>
        /// <param name="personalInformation">Personal information (gender, height, etc)</param>
        /// <returns>Created personal information</returns>
        // POST: api/PersonalInformations
        [ProducesResponseType( typeof(PersonalInformation),StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<App.Public.DTO.v1.PersonalInformation>> PostPersonalInformation(
            App.Public.DTO.v1.PersonalInformation personalInformation)
        {
            var data = _personalInformationMapper.Map(personalInformation);

            data!.AppUserId = User.GetUserId();

            _appBll.PersonalInformationService.Add(data);

            await _appBll.SaveChangesAsync();

            personalInformation.Id = data.Id;

            return CreatedAtAction("GetPersonalInformation", new {id = personalInformation.Id}, personalInformation);
        }
    }
}