using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;



namespace WebApplication8.Controllers
{
    /// <summary>All the HTTP response are returned in this controller class.</summary>
    [Route("api/groups")]
    [ApiController]
    public class GroupController : Controller                       //controller class containing api methods definition
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="GroupController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public GroupController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>Gets all groups.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllGroups()
        {

            try
            {
                var groups = _repository.Group.GetAllGroups();

                _logger.LogInfo($"Returned all groups from database.");

                return Ok(groups);                          //returning the group objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllGroups action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the group by id.</summary>
        /// <param name="gid">The id of the group to be searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/groups/{id:int}")]
        [HttpGet("{gid:int}", Name="GroupById")]
        public IActionResult GetGroupById(int gid)
        {
            try
            {
                var group = _repository.Group.GetGroupById(gid);

                if (group.Name== null && group.id == 0)
                {
                    _logger.LogError($"Group with id: {gid}, hasn't been found in db.");
                    return NotFound();                           //logging the error with NotFound status code.
                }
                else
                {
                    _logger.LogInfo($"Returned group with id: {gid}");
                    return Ok(group);                            //returning the group objects with OK return code.

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGroupByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the group by name.</summary>
        /// <param name="gName">Name of the group to searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/groups/{Name}")]
        [HttpGet("{gName}")]
        public IActionResult GetGroupByName(string gName)
        {
            try
            {
                var group = _repository.Group.GetGroupByName(gName);

                if (group.Name == null && group.id == 0)
                {
                    _logger.LogError($"Group with Name: {gName}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned group with Name: {gName}");
                    return Ok(group);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetGroupByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets all associate groups.</summary>
        /// <param name="AssocID">The associate id.</param>
        /// <returns>IActionResult</returns>
        [Route("/api/groupsFor/AssocID")]
        [HttpGet]
        public IActionResult GetAllAssocGroups(string AssocID)
        {

            try
            {
                var groups = _repository.Group.GetAllGroups();

                _logger.LogInfo($"Returned all groups from database.");

                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllGroups action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Creates the group.</summary>
        /// <param name="group">The group object from the form.</param>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult CreateGroup([FromBody]Group group)
        {
            try
            {
                if (group == null)                      //if the group object passed is null.
                {
                    _logger.LogError("Group object sent from client is null.");
                    return BadRequest("Group object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of groupForm is invalid.
                {
                    _logger.LogError("Invalid group object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Group.CreateGroup(group);               //creates a group entry in db using repositoryContext.

                return RedirectToAction("opportunities", "admin");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateGroup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>Updates the group.</summary>
        /// <param name="id">The id of the group to be updated.</param>
        /// <param name="group">The group object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdateGroup(int id, [FromBody]Group group)
        {
            try
            {
                if (group == null)
                {
                    _logger.LogError("Group object sent from client is null.");
                    return BadRequest("Group object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid group object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbGroup = _repository.Group.GetGroupById(id);
                if (dbGroup.Name == null && dbGroup.id == 0)
                {
                    _logger.LogError($"Group with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Group.UpdateGroup(dbGroup, group);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateGroup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Deletes the group.</summary>
        /// <param name="id">The id of the group to be deleted.</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(int id)
        {
            try
            {
                var group = _repository.Group.GetGroupById(id);
                if (group.Name == null && group.id == 0)              //if the group is not found in db.
                {
                    _logger.LogError($"Group with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Group.DeleteGroup(group);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteGroup action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
