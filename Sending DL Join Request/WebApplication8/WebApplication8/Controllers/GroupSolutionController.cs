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
    /// <summary>Get all groups and solutions.</summary>
    [Route("api/groupSolution")]
    [ApiController]
    public class GroupSolutionController : ControllerBase
    {
        /// <summary>Linking solution to group.</summary>
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="AssociateController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public GroupSolutionController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;

        }

        /// <summary>Gets all groups and soltuions.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllGroupSolutions()
        {

            try
            {
                var groups = _repository.GroupSolution.GetAllGroupSolutions();

                _logger.LogInfo($"Returned all groups from database.");

                return Ok(groups);                          //returning the group objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllAssociate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the group by id.</summary>
        /// <param name="gid">The id of the group to be searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/groupSolution/{id:int}")]
        [HttpGet("{gid:int}", Name = "GetAllSolutionsByGroupId")]
        public IActionResult GetAllSolutionsByGroupId(int gid)
        {
            try
            {
                var groupSolution = _repository.GroupSolution.GetAllSolutionsByGroupId(gid);

                if (groupSolution == null)
                {
                    _logger.LogError($"Group with id: {gid}, hasn't been found in db.");
                    return NotFound();                           //logging the error with NotFound status code.
                }
                else
                {
                    _logger.LogInfo($"Returned group with id: {gid}");
                    return Ok(groupSolution);                            //returning the group objects with OK return code.

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSolutionsByGroupId action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Creates the group.</summary>
        /// <param name="groupSolutionHelper">The group object from the form.</param>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult CreateGroupSolution([FromBody]GroupSolutionHelper groupSolutionHelper)
        {
            try
            {
                if (groupSolutionHelper == null)                      //if the group object passed is null.
                {
                    _logger.LogError("GroupSolution object sent from client is null.");
                    return BadRequest("GroupSolution object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of groupForm is invalid.
                {
                    _logger.LogError("Invalid groupSolution object sent from client.");
                    return BadRequest("Invalid model object");
                }

                //string[] Solutions = groupSolutionHelper.solutionString.Split(',');

                for(int i=0; i<groupSolutionHelper.solutionArray.Length; i++)
                {
                    var groupSolution = new GroupSolution();
                    groupSolution.GroupId = groupSolutionHelper.groupId;
                    var solution = _repository.Solution.GetSolutionById(groupSolutionHelper.solutionArray[i]);
                    groupSolution.SolutionId = solution.SolutionId;
                    _repository.GroupSolution.CreateGroupSolution(groupSolution);
                }
                return Ok();                          //returning the group objects with OK return code.

                //return CreatedAtRoute("GroupSolutionById", new { gid = groupSolution.gs_id }, groupSolution);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateGroupSolution action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}