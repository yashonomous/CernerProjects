using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    /// <summary>All the HTTP response are returned in this controller class.</summary>
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="TeamController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public TeamController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>Gets all teams.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllTeams()
        {

            try
            {
                var teams = _repository.Team.GetAllTeams();

                _logger.LogInfo($"Returned all teams from database.");

                return Ok(teams);                          //returning the team objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllTeams action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the team by id.</summary>
        /// <param name="gid">The id of the team to be searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/teams/{id:int}")]
        [HttpGet("{gid:int}", Name = "TeamById")]
        public IActionResult GetTeamById(int gid)
        {
            try
            {
                var team = _repository.Team.GetTeamById(gid);

                if (team == null)
                {
                    _logger.LogError($"team with id: {gid}, hasn't been found in db.");
                    return NotFound();                           //logging the error with NotFound status code.
                }
                else
                {
                    _logger.LogInfo($"Returned team with id: {gid}");
                    return Ok(team);                            //returning the team objects with OK return code.

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeamByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the team by name.</summary>
        /// <param name="gName">Name of the team to searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/teams/{Name}")]
        [HttpGet("{gName}")]
        public IActionResult GetTeamByName(string gName)
        {
            try
            {
                var team = _repository.Team.GetTeamByName(gName);

                if (team == null)
                {
                    _logger.LogError($"Team with Name: {gName}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned team with Name: {gName}");
                    return Ok(team);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeamByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets all associate teams.</summary>
        /// <param name="AssocID">The associate id.</param>
        /// <returns>IActionResult</returns>
        [Route("/api/teamsFor/AssocID")]
        [HttpGet]
        public IActionResult GetAllAssocTeams(string AssocID)
        {

            try
            {
                var teams = _repository.Team.GetAllTeams();

                _logger.LogInfo($"Returned all teams from database.");

                return Ok(teams);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTeams action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Creates the team.</summary>
        /// <param name="team">The team object from the form.</param>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult CreateTeam([FromBody]Team team)
        {
            try
            {
                if (team == null)                      //if the team object passed is null.
                {
                    _logger.LogError("Team object sent from client is null.");
                    return BadRequest("Team object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of teamForm is invalid.
                {
                    _logger.LogError("Invalid team object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Team.CreateTeam(team);               //creates a team entry in db using repositoryContext.

                return CreatedAtRoute("TeamById", new { gid = team.TeamId }, team);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTeam action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>Updates the team.</summary>
        /// <param name="id">The id of the team to be updated.</param>
        /// <param name="team">The team object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdateTeam(int id, [FromBody]Team team)
        {
            try
            {
                if (team == null)
                {
                    _logger.LogError("Team object sent from client is null.");
                    return BadRequest("Team object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid team object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbTeam = _repository.Team.GetTeamById(id);
                if (dbTeam == null)
                {
                    _logger.LogError($"Team with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Team.UpdateTeam(dbTeam, team);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTeam action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Deletes the team.</summary>
        /// <param name="id">The id of the team to be deleted.</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteTeam(int id)
        {
            try
            {
                var team = _repository.Team.GetTeamById(id);
                if (team == null)              //if the team is not found in db.
                {
                    _logger.LogError($"Team with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Team.DeleteTeam(team);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTeam action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}