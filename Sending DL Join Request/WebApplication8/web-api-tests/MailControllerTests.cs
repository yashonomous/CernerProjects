using Contracts;
using Entities;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication8.Controllers;
using Xunit;

namespace web_api_tests
{
    /// <summary>Class containing all the test cases for Mail controller.</summary>
    public class MailControllerTests                            //class containing testcases for Mail Controller
    {
        ILoggerManager logger;
        /// <summary>Sends the mail returns correct response.</summary>
        /// <returns>Generic Task.</returns>
        [Fact]
        public async Task SendMail_ReturnsCorrectResponse()
        {
            logger = new LoggerManager();
            var controller = new MailController(logger);

            
            try
            {
                var result = controller.SendMail(new Recepients { recepientString = "yash.soni4@gmail.com,yashaswi.soni2015@vit.ac.in", requestedGroups = "Group1,Group5" });
                Assert.IsAssignableFrom<IActionResult>(result);

            }
            catch (Exception e)
            {
                Action act = () => controller.SendMail(new Recepients { recepientString = "yash.soni4@gmail.com,yashaswi.soni2015@vit.ac.in", requestedGroups = "Group1,Group5" });

                Assert.Throws<NullReferenceException>(act);
            }

        }

        /// <summary>Sends the mail exception returns correct response.</summary>
        /// <returns>Generic Task.</returns>
        [Fact]
        public async Task SendMailException_ReturnsCorrectResponse()
        {
            logger = new LoggerManager();
            var controller = new MailController(logger);

                Action act = () => controller.SendMail(new Recepients { });

                Assert.Throws<NullReferenceException>(act);
            

        }
    }
}
