using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace OpportunityManagement.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        public static string _loggedInUser;
        private static Login login_details;
        /// <summary>Initializes a new instance of the <see cref="GroupController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The repository.</param>
        public UserController(ILoggerManager logger, IRepositoryWrapper repository)             //setting logger and repository objects
        {
            _logger = logger;
            _repository = repository;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            ViewBag.loggedIn = _loggedInUser;
            return View("Login");
        }

        [HttpGet]
        [Route("")]
        public IActionResult Login()
        {
            ViewBag.loggedIn = _loggedInUser;
            return View("Login");
        }

        [HttpGet]
        [Route("GetUser/{UserName}")]
        public IActionResult GetUser(string UserName)
        {
            var user = _repository.User.GetUserByName(UserName);
            return new JsonResult(user);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Login(Login login)
        {
            try {
                var encryptedPassword = Encrypt(login.Password);

                var isExist = _repository.User.checkUserExists(login.UserName, encryptedPassword);

                _loggedInUser = login.UserName;

                login_details = login;

                ViewBag.loggedIn = _loggedInUser;

                var user = _repository.User.GetUserByName(login.UserName);

                if (isExist == true)
                {
                    _logger.LogInfo($"User exist in db.");

                    switch (user.Role)
                    {
                        case "Admin":
                            _logger.LogInfo($"Admin logged in as " + login.UserName);
                            TempData["Success"] = "Logged in as " + login.UserName;
                            return View("../NavBar");
                        case "Doctor":
                            var oppr = _repository.Opportunity.GetAllOpportunities().Where(opp => opp.IsVacant.Equals("true"));
                            var oppr1 = _repository.User_Opportunity.GetAllOpportunitiesByUserId(_repository.User.GetUserByName(login.UserName).user_id);
                            var oppList = new List<Opp_Display>();
                            foreach(var ptr in oppr)
                            {
                                Opp_Display oppr_update = new Opp_Display();
                                oppr_update.o_id = ptr.o_id;
                                oppr_update.Opportunity_Description = ptr.OpportunityDescription;
                                oppr_update.Start_Date = ptr.StartTime;
                                oppr_update.End_Date = ptr.EndTime;
                                foreach(var ptr1 in oppr1)
                                {
                                    if ( (ptr1.Opportunity_Id == ptr.o_id) && (ptr1.Is_Accepted == "false") )
                                    {
                                        oppr_update.Is_Accepted = "Pending";
                                        //oppList.Add(oppr_update);
                                        break;
                                    }
                                    else if((ptr1.Opportunity_Id == ptr.o_id) && (ptr1.Is_Accepted == "true"))
                                    {
                                        oppr_update.Is_Accepted = "Approved";
                                        //oppList.Add(oppr_update);
                                        break;
                                    }
                                    oppr_update.Is_Accepted = "Vacant";
                                   

                                }
                                oppList.Add(oppr_update);
                            }

                            ViewBag.opportunities = oppList;
                            TempData["Success"] = "Logged in as " + login.UserName;
                            _logger.LogInfo($"Doctor logged in as " + login.UserName);
                            return View("Index");
                        case "Nurse":

                            var opprt = _repository.Opportunity.GetAllOpportunities().Where(opp => opp.IsVacant.Equals("true"));
                            var opprt1 = _repository.User_Opportunity.GetAllOpportunitiesByUserId(_repository.User.GetUserByName(login.UserName).user_id);
                            var opptList = new List<Opp_Display>();
                            foreach (var ptr in opprt)
                            {
                                Opp_Display oppr_update = new Opp_Display();
                                oppr_update.o_id = ptr.o_id;
                                oppr_update.Opportunity_Description = ptr.OpportunityDescription;
                                oppr_update.Start_Date = ptr.StartTime;
                                oppr_update.End_Date = ptr.EndTime;
                                foreach (var ptr1 in opprt1)
                                {
                                    if ((ptr1.Opportunity_Id == ptr.o_id) && (ptr1.Is_Accepted == "false"))
                                    {
                                        oppr_update.Is_Accepted = "Pending";
                                        //oppList.Add(oppr_update);
                                        break;
                                    }
                                    else if ((ptr1.Opportunity_Id == ptr.o_id) && (ptr1.Is_Accepted == "true"))
                                    {
                                        oppr_update.Is_Accepted = "Approved";
                                        //oppList.Add(oppr_update);
                                        break;
                                    }
                                    oppr_update.Is_Accepted = "Vacant";


                                }
                                opptList.Add(oppr_update);
                            }
                            
                            ViewBag.opportunities = opptList;
                            TempData["Success"] = "Logged in as " + login.UserName;
                            _logger.LogInfo($"Nurse logged in as " + login.UserName);
                            return View("Index");

                    }                       //returning the group objects with OK return code.
                }

                _logger.LogInfo($"User doesn't exist in db.");
                TempData["Error"] = "User doesn't exist.";
                return NotFound("User doesn't exist in DB.");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrong inside Login action: {ex.Message}");
                return StatusCode(500, "Server didn't respond.");
            }
            
            
        }
        [HttpGet]
        [Route("apply/{id}")]
        public IActionResult Apply(int id)
        {
            try
            {
                var opportunity = _repository.Opportunity.GetOpportunityById(id);
                var user = _repository.User.GetUserByName(_loggedInUser);
                var user_opportunity = new User_Opportunity { User_Id = user.user_id, Opportunity_Id = opportunity.o_id, Is_Accepted = "false", Request_Date= DateTime.Today };
                if ((opportunity.OpportunityDescription == null && opportunity.o_id == 0) || 
                    (user.user_id == 0 && user.UserName == null))              //if the opportunity is not found in db.
                {
                    _logger.LogError($"opportunity with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                var oppr = _repository.Opportunity.GetAllOpportunities().Where(opp => opp.IsVacant.Equals("true"));
                var oppr1 = _repository.User_Opportunity.GetAllOpportunitiesByUserId(user.user_id);
                var oppList = new List<Opp_Display>();
                foreach (var ptr in oppr)
                {
                    Opp_Display oppr_update = new Opp_Display();
                    oppr_update.o_id = ptr.o_id;
                    oppr_update.Opportunity_Description = ptr.OpportunityDescription;
                    oppr_update.Start_Date = ptr.StartTime;
                    oppr_update.End_Date = ptr.EndTime;
                    foreach (var ptr1 in oppr1)
                    {
                        if ((ptr1.Opportunity_Id == ptr.o_id) && (ptr1.Is_Accepted == "false"))
                        {
                            oppr_update.Is_Accepted = "Pending";
                            //oppList.Add(oppr_update);
                            break;
                        }
                        else if ((ptr1.Opportunity_Id == ptr.o_id) && (ptr1.Is_Accepted == "true"))
                        {
                            oppr_update.Is_Accepted = "Approved";
                            //oppList.Add(oppr_update);
                            break;
                        }
                        oppr_update.Is_Accepted = "Vacant";


                    }
                    oppList.Add(oppr_update);
                }

                ViewBag.opportunities = oppList;
                _logger.LogInfo($"Returned all vacant opportunities from database.");

                _repository.User_Opportunity.CreateUserOpportunity(user_opportunity);

                return View("Index");
                //return RedirectToAction("Login(login_details)", "User");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOpportunity action: {ex.Message}");
                return StatusCode(500, "Already applied for the opportunity");
            }
        }

        public static string Encrypt(string value)
        {
            using (MD5CryptoServiceProvider md5Obj = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5Obj.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
    }
}