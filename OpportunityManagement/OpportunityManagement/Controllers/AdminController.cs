using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OpportunityManagement.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {

        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        /// <summary>Initializes a new instance of the <see cref="AdminController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public AdminController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>Get all opportunities.</summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("opportunities")]
        public IActionResult Admin()
        {
            ViewBag.opportunities = _repository.Opportunity.GetAllOpportunities();
            _logger.LogInfo($"Returned all users from database.");
            return View("Admin");
        }



        /// <summary>Display view for opportunity entry in DB.</summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View("Create");
        }


        /// <summary>Display view for opportunity entry in DB.</summary>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("requests")]
        public IActionResult Requests()
        {
            try
            {
                ViewBag.opportunities = _repository.Opportunity.GetAllOpportunities();
                var events = _repository.Opportunity.GetAllOpportunities();

                //var date = DateTime.Today;
                return View("Request");
                //return RedirectToAction("CreateDate");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateDate action: {ex.Message}");
                
                //throw new Exception("Server Error!");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpGet]
        [Route("GetEvents")]
        public IActionResult GetEvents()
        {
            try { 
                var events = _repository.Opportunity.GetAllOpportunities();
                var user_opportunity = _repository.User_Opportunity.GetAllUserOpportunities();

                List<Opportunity> opportunities = new List<Opportunity> { };
                List<User> users = new List<User> { };
                List<Requested> requested = new List<Requested> { }; 
                foreach (var user in user_opportunity)
                {
                    var opp = _repository.Opportunity.GetOpportunityById(user.Opportunity_Id);
                    var usr = _repository.User.GetUserById(user.User_Id);
                    opportunities.Add(opp);
                    users.Add(usr);
                    var requestedOpp = new Requested
                    {
                        user_opportunity_id = user.user_opportunity_id,
                        Opportunity_Description = opp.OpportunityDescription,
                        User_Name = usr.UserName,
                        Start_Date = user.Request_Date,
                        Color = opp.Color
                    };
                    requested.Add(requestedOpp);
                }
                //var x = requested.FindAll(c => c.Start_Date.Equals(Convert.ToDateTime(startDate)));
                return new JsonResult(requested);

                //return new JsonResult(events);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>Create the opportunity.</summary>
        /// <param name="opportunity">The opportunity object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("CreateDate/{startDate}")]
        public IActionResult CreateDate(string startDate)
        {
            try { 
                var result = _repository.Opportunity.FindByCondition(opp => opp.StartTime.Equals(Convert.ToDateTime(startDate)));
                var user_opportunity = _repository.User_Opportunity.GetAllUserOpportunities();
                List<Opportunity> opportunities = new List<Opportunity> { };
                List<User> users = new List<User> { };
                List<Requested> requested = new List<Requested> { };
                foreach (var user in user_opportunity)
                {
                    var opp = _repository.Opportunity.GetOpportunityById(user.Opportunity_Id);
                    var usr = _repository.User.GetUserById(user.User_Id);
                    opportunities.Add(opp);
                    users.Add(usr);
                    var requestedOpp = new Requested
                    {
                        user_opportunity_id = user.user_opportunity_id,
                        Opportunity_Description = opp.OpportunityDescription,
                        User_Name = usr.UserName,
                        Start_Date = user.Request_Date,
                        Is_Accepted = user.Is_Accepted
                    };
                    requested.Add(requestedOpp);  
                }
                //var x = requested.FindAll(c => (c.Start_Date.Equals(Convert.ToDateTime(startDate)) && c.Is_Accepted.Equals("false")));
                var x = requested.FindAll(c => c.Start_Date.Equals(Convert.ToDateTime(startDate)));
                return new JsonResult(x);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateDate action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

        /// <summary>Create the opportunity.</summary>
        /// <param name="opportunity">The opportunity object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("create")]
        public IActionResult Create(Opportunity opportunity)
        {
            try
            {
                //if (opportunity == null)                      //if the opportunity object passed is null.
                //{
                //    _logger.LogError("opportunity object sent from client is null.");
                //    return BadRequest("opportunity object is null");              //return BadRequest status code.
                //}

                if (!ModelState.IsValid)                //if the ModelState of opportunityForm is invalid.
                {
                    _logger.LogError("Invalid opportunity object sent from client.");
                    return BadRequest("Invalid model object");
                }
                opportunity.Color = "red";

                _repository.Opportunity.CreateOpportunity(opportunity);               //creates a opportunity entry in db using repositoryContext.

                return RedirectToAction("opportunities", "admin");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOpportunity action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Get view for updating the opportunity.</summary>
        /// <param name="id">The id of the opportunity to be updated.</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("update/{id}")]
        public IActionResult Update(int id)
        {
            try { 
                var a = _repository.Opportunity.GetOpportunityById(id);
                var formatted_date = a.StartTime.ToString("yyyy-MM-dd");
                return View("Update", a);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside Update action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Updates the opportunity.</summary>
        /// <param name="id">The id of the opportunity to be updated.</param>
        /// <param name="opportunity">The opportunity object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        [Route("update/{id:int}")]
        public IActionResult Update(int id, Opportunity opportunity)
        {
            try
            {
                if (opportunity == null)
                {
                    _logger.LogError("opportunity object sent from client is null.");
                    return BadRequest("opportunity object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid opportunity object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbOpportunity = _repository.Opportunity.GetOpportunityById(id);
                if (dbOpportunity.OpportunityDescription == null && dbOpportunity.o_id == 0)
                {
                    _logger.LogError($"opportunity with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Opportunity.UpdateOpportunity(dbOpportunity, opportunity);
                _logger.LogError("data updated");
                return RedirectToAction("opportunities", "admin");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOpportunity action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>Updates the opportunity.</summary>
        /// <param name="id">The id of the opportunity to be updated.</param>
        /// <param name="opportunity">The opportunity object from the form.</param>
        /// <returns>IActionResult</returns>
        [HttpGet]
        [Route("updateRequest/{id:int}")]
        public IActionResult UpdateRequestStatus(int id)
        {
            try
            {

                var dbUserOpportunity = _repository.User_Opportunity.GetUserOpportunityById(id);
                if (dbUserOpportunity.Opportunity_Id == 0 && dbUserOpportunity.User_Id == 0)
                {
                    _logger.LogError($"opportunity with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var userOpportunity = new User_Opportunity { user_opportunity_id = dbUserOpportunity.user_opportunity_id,
                                                            Opportunity_Id = dbUserOpportunity.Opportunity_Id,
                                                            User_Id = dbUserOpportunity.User_Id,
                                                            Is_Accepted = "true",
                                                            Request_Date = dbUserOpportunity.Request_Date};
                var dbOpportunity = _repository.Opportunity.GetOpportunityById(dbUserOpportunity.Opportunity_Id);
                var opportunity = dbOpportunity;
                opportunity.Color = "green";
                _repository.Opportunity.UpdateOpportunity(dbOpportunity, opportunity);
                _repository.User_Opportunity.UpdateUserOpportunity(dbUserOpportunity, userOpportunity);
                _logger.LogError("data updated");
                return RedirectToAction("opportunities", "admin");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOpportunity action: {ex.Message}");
                return StatusCode(500, "Request already approved for this opportunity.");
            }
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var opportunity = _repository.Opportunity.GetOpportunityById(id);
                if (opportunity.OpportunityDescription == null && opportunity.o_id== 0)              //if the opportunity is not found in db.
                {
                    _logger.LogError($"opportunity with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Opportunity.DeleteOpportunity(opportunity);

                return RedirectToAction("opportunities", "admin");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOpportunity action: {ex.Message}");
                return StatusCode(500, "Internal server error \n" + ex);
            }
        }
        
    }
}