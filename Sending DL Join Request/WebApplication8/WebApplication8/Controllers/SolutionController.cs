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
    [Route("api/solutions")]
    [ApiController]
    public class SolutionController : Controller                       //controller class containing api methods definition
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="SolutionController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public SolutionController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>Gets all solutions.</summary>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpGet]
        public IActionResult GetAllSolutions()
        {

            try
            {
                var solutions = _repository.Solution.GetAllSolutions();

                _logger.LogInfo($"Returned all solutions from database.");

                return Ok(solutions);                          //returning the solution objects with OK return code.

            }
            catch (Exception ex)                //catching exception and logging it with error return code.
            {
                _logger.LogError($"Something went wrong inside GetAllsolutions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the solution by id.</summary>
        /// <param name="gid">The id of the solution to be searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/solutions/{id:int}")]
        [HttpGet("{gid:int}", Name = "SolutionById")]
        public IActionResult GetSolutionById(int gid)
        {
            try
            {
                var solution = _repository.Solution.GetSolutionById(gid);

                if (solution == null)
                {
                    _logger.LogError($"Solution with id: {gid}, hasn't been found in db.");
                    return NotFound();                           //logging the error with NotFound status code.
                }
                else
                {
                    _logger.LogInfo($"Returned solution with id: {gid}");
                    return Ok(solution);                            //returning the solution objects with OK return code.

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSolutionByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets the solution by name.</summary>
        /// <param name="gName">Name of the solution to searched.</param>
        /// <returns>IActionResult</returns>
        [Route("api/solutions/{Name}")]
        [HttpGet("{gName}")]
        public IActionResult GetSolutionByName(string gName)
        {
            try
            {
                var solution = _repository.Solution.GetSolutionByName(gName);

                if (solution == null)
                {
                    _logger.LogError($"Solution with Name: {gName}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned solution with Name: {gName}");
                    return Ok(solution);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetSolutionByName action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Gets all associate solutions.</summary>
        /// <param name="AssocID">The associate id.</param>
        /// <returns>IActionResult</returns>
        [Route("/api/solutionsFor/AssocID")]
        [HttpGet]
        public IActionResult GetAllAssocSolutions(string AssocID)
        {

            try
            {
                var solutions = _repository.Solution.GetAllSolutions();

                _logger.LogInfo($"Returned all solutions from database.");

                return Ok(solutions);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllSolutions action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Creates the solution.</summary>
        /// <param name="solution">The solution object from the form.</param>
        /// <returns>IActionResult</returns>
        [Route("")]
        [HttpPost]
        public IActionResult Createsolution([FromBody]Solution solution)
        {
            try
            {
                if (solution == null)                      //if the solution object passed is null.
                {
                    _logger.LogError("Solution object sent from client is null.");
                    return BadRequest("Solution object is null");              //return BadRequest status code.
                }

                if (!ModelState.IsValid)                //if the ModelState of solutionForm is invalid.
                {
                    _logger.LogError("Invalid solution object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Solution.CreateSolution(solution);               //creates a solution entry in db using repositoryContext.

                return CreatedAtRoute("SolutionById", new { gid = solution.SolutionId }, solution);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Createsolution action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>Updates the solution.</summary>
        /// <param name="id">The id of the solution to be updated.</param>
        /// <param name="solution">The solution object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpPut("{id:int}")]
        public IActionResult Updatesolution(int id, [FromBody]Solution solution)
        {
            try
            {
                if (solution == null)
                {
                    _logger.LogError("Solution object sent from client is null.");
                    return BadRequest("Solution object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid solution object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbsolution = _repository.Solution.GetSolutionById(id);
                if (dbsolution == null)
                {
                    _logger.LogError($"Solution with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Solution.UpdateSolution(dbsolution, solution);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateSolution action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Deletes the solution.</summary>
        /// <param name="id">The id of the solution to be deleted.</param>
        /// <returns>IActionResult</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletesolution(int id)
        {
            try
            {
                var solution = _repository.Solution.GetSolutionById(id);
                if (solution == null)              //if the solution is not found in db.
                {
                    _logger.LogError($"Solution with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Solution.DeleteSolution(solution);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteSolution action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

    