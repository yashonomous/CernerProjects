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
    /// <summary>Get all groups and Teams.</summary>
    [Route("api/groupTeam")]
    [ApiController]
    public class GroupTeamController : ControllerBase
    {
        /// <summary>Linking Team to group.</summary>
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="GroupTeamController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public GroupTeamController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;

        }

        /// <summary>Gets all groups and soltuions.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllGroupTeams()
        {

            try
            {
                var groups = _repository.GroupTeam.GetAllGroupTeams();

                _logger.LogInfo($"Returned all groups and teams from database.");

                return Ok(groups);                          //returning the group objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllGroupTeams action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the group by id.</summary>
        /// <param name="gid">The id of the group to be searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/groupTeam/{id:int}")]
        [HttpGet("{gid:int}", Name = "GetAllTeamsByGroupId")]
        public IActionResult GetAllTeamsByGroupId(int gid)
        {
            try
            {
                var groupTeam = _repository.GroupTeam.GetAllTeamsByGroupId(gid);

                if (groupTeam == null)
                {
                    _logger.LogError($"Group with id: {gid}, hasn't been found in db.");
                    return NotFound();                           //logging the error with NotFound status code.
                }
                else
                {
                    _logger.LogInfo($"Returned group with id: {gid}");
                    return Ok(groupTeam);                            //returning the group objects with OK return code.

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTeamsByGroupId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Creates the group.</summary>
        /// <param name="groupTeamHelper">The group object from the form.</param>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult CreateGroupTeam([FromBody]GroupTeamHelper groupTeamHelper)
        {
            try
            {
                if (groupTeamHelper == null)                      //if the group object passed is null.
                {
                    _logger.LogError("GroupTeam object sent from client is null.");
                    return BadRequest("GroupTeam object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of groupForm is invalid.
                {
                    _logger.LogError("Invalid groupTeam object sent from client.");
                    return BadRequest("Invalid model object");
                }

                //string[] Teams = groupTeamHelper.TeamString.Split(',');

                for (int i = 0; i < groupTeamHelper.teamArray.Length; i++)
                {
                    var groupTeam = new GroupTeam();
                    groupTeam.GroupId = groupTeamHelper.groupId;
                    var Team = _repository.Team.GetTeamById(groupTeamHelper.teamArray[i]);
                    groupTeam.TeamId = Team.TeamId;
                    _repository.GroupTeam.CreateGroupTeam(groupTeam);
                }
                return Ok();                          //returning the group objects with OK return code.

                //return CreatedAtRoute("GroupTeamById", new { gid = groupTeam.gs_id }, groupTeam);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateGroupTeam action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}