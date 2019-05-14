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
    /// <summary>Checks if the associate exists in db.</summary>
    [Route("api/associates")]
    [ApiController]
    public class AssociateController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="AssociateController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public AssociateController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>Gets all groups.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllAssociates()
        {

            try
            {
                var groups = _repository.Associate.GetAllAssociates();

                _logger.LogInfo($"Returned all groups from database.");

                return Ok(groups);                          //returning the group objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllAssociate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets all groups.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult checkAssociateExists([FromBody]Associate associate )
        {

            try
            {
                if (associate == null)                      //if the group object passed is null.
                {
                    _logger.LogError("Associate object sent from client is null.");
                    return BadRequest("Associate object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of groupForm is invalid.
                {
                    _logger.LogError("Invalid associate object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var assoc = _repository.Associate.checkAssociateExists(associate.AssociateId);

                if (assoc == true)
                {
                    _logger.LogInfo($"User exist in db.");

                    return Ok();                          //returning the group objects with OK return code.
                }

                _logger.LogInfo($"User doesn't exist in db.");
                return NotFound();
            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside checkAssociateExists action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}