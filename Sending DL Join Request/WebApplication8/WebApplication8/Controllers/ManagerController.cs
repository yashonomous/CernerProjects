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
    [Route("api/managers")]
    [ApiController]
    public class ManagerController : ControllerBase
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="ManagerController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public ManagerController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>Gets all managers.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllManagers()
        {

            try
            {
                var managers = _repository.Manager.GetAllManagers();

                _logger.LogInfo($"Returned all managers from database.");

                return Ok(managers);                          //returning the manager objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllManagers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the manager by id.</summary>
        /// <param name="gid">The id of the manager to be searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/managers/{id:int}")]
        [HttpGet("{gid:int}", Name = "ManagerById")]
        public IActionResult GetManagerById(int gid)
        {
            try
            {
                var manager = _repository.Manager.GetManagerById(gid);

                if (manager == null)
                {
                    _logger.LogError($"Manager with id: {gid}, hasn't been found in db.");
                    return NotFound();                           //logging the error with NotFound status code.
                }
                else
                {
                    _logger.LogInfo($"Returned manager with id: {gid}");
                    return Ok(manager);                            //returning the manager objects with OK return code.

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetManagerByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the manager by name.</summary>
        /// <param name="gName">Name of the manager to searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/managers/{Name}")]
        [HttpGet("{gName}")]
        public IActionResult GetManagerByName(string gName)
        {
            try
            {
                var manager = _repository.Manager.GetManagerByName(gName);

                if (manager == null)
                {
                    _logger.LogError($"Manager with Name: {gName}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned manager with Name: {gName}");
                    return Ok(manager);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetManagerByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets all associate managers.</summary>
        /// <param name="AssocID">The associate id.</param>
        /// <returns>IActionResult</returns>
        [Route("/api/managersFor/AssocID")]
        [HttpGet]
        public IActionResult GetAllAssocManagers(string AssocID)
        {

            try
            {
                var managers = _repository.Manager.GetAllManagers();

                _logger.LogInfo($"Returned all managers from database.");

                return Ok(managers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllManagers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Creates the manager.</summary>
        /// <param name="manager">The manager object from the form.</param>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult CreateManager([FromBody]Manager manager)
        {
            try
            {
                if (manager == null)                      //if the manager object passed is null.
                {
                    _logger.LogError("Manager object sent from client is null.");
                    return BadRequest("Manager object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of managerForm is invalid.
                {
                    _logger.LogError("Invalid manager object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Manager.CreateManager(manager);               //creates a manager entry in db using repositoryContext.

                return CreatedAtRoute("ManagerById", new { gid = manager.ManagerId }, manager);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateManager action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>Updates the manager.</summary>
        /// <param name="id">The id of the manager to be updated.</param>
        /// <param name="manager">The manager object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id:int}")]
        public IActionResult UpdateManager(int id, [FromBody]Manager manager)
        {
            try
            {
                if (manager == null)
                {
                    _logger.LogError("Manager object sent from client is null.");
                    return BadRequest("Manager object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid manager object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbManager = _repository.Manager.GetManagerById(id);
                if (dbManager == null)
                {
                    _logger.LogError($"manager with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Manager.UpdateManager(dbManager, manager);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateManager action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Deletes the manager.</summary>
        /// <param name="id">The id of the manager to be deleted.</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletemanager(int id)
        {
            try
            {
                var manager = _repository.Manager.GetManagerById(id);
                if (manager == null)              //if the manager is not found in db.
                {
                    _logger.LogError($"Manager with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Manager.DeleteManager(manager);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteManager action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}