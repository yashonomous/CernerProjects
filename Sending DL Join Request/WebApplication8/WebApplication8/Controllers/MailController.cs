using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication8.Controllers
{
    /// <summary>Controller class for sending mail to the admins using SMTP.</summary>
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase //controller class having api functions for sending mai
    {
        private ILoggerManager _logger;

        /// <summary>Initializes a new instance of the <see cref="MailController"/> class.</summary>
        /// <param name="logger">The logger.</param>
        public MailController(ILoggerManager logger)             //setting logger and repository objects
        {
            _logger = logger;
        }

        /// <summary>Sends the mail.</summary>
        /// <param name="recipients">The recipients.</param>
        /// <returns>IActionResult</returns>
        [HttpPost]
        public IActionResult
            SendMail([FromBody] Recepients recipients) //passing the recipient object having recipients and groups strings.
        {

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtprr.cerner.com";
            client.Port = 25;

            // setup Smtp authentication automatically
            client.UseDefaultCredentials = true;

            string[] Recipients = recipients.recepientString.Split(',');
            string[] Groups = recipients.requestedGroups.Split(',');
            bool[] visited_location = new bool[Groups.Length]; // create an array for keeping track of visiting some other array

            for (int i = 0; i < Groups.Length; ++i)
            {
                visited_location[i] = false; // set all to false to begin
            }

            //below code will map all groups to their respective admins and send a
            //single mail containing details of all the requested groups.
            try
            {
                for (int i = 0; i < Recipients.Length; i++)
                {
                    int flag = 0;
                    string Body = "New Associate has requested to join groups:"; //body for our mail.
                    for (int j = 0; j < Recipients.Length; j++)
                    {
                        if (Recipients[j] == Recipients[i] && visited_location[j] != true)
                        {
                            visited_location[j] = true;
                            Body = Body + "\n" + Groups[j]; //adding requested groups to the body.
                            flag = 1;
                        }

                    }


                    if (flag == 1)
                    {
                        var rec = new MailAddress(Recipients[i]);
                        MailMessage msg = new MailMessage("yashaswi.soni@cerner.com", Recipients[i],
                            "New Group join Request from Associate.", Body); //creating a mail object.
                        msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        msg.Headers.Add("Disposition-Notification-To", "yashaswi.soni@cerner.com");
                        try
                        {
                            _logger.LogError($"Email sent to: {Recipients[i]}");
                            client.Send(msg); //sending the mail.

                        }
                        catch (SmtpFailedRecipientsException ex) //catching exceptions while sending mail.
                        {
                            for (int k = 0; k < ex.InnerExceptions.Length; k++)
                            {
                                SmtpStatusCode status = ex.InnerExceptions[k].StatusCode;
                                if (status == SmtpStatusCode.MailboxBusy ||
                                    status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    Debug.WriteLine("Delivery failed - retrying in 5 seconds.");
                                    System.Threading.Thread.Sleep(5000);
                                    _logger.LogError($"Email sent to: {Recipients[i]}");
                                    client.Send(msg);

                                }
                                else
                                {
                                    Debug.WriteLine("Failed to deliver message to {0}",
                                        ex.InnerExceptions[k].FailedRecipient);
                                    _logger.LogError($"Email could not be sent to: {Recipients[i]}");
                                }

                            }
                        }
                    }
                }

                return Ok(recipients);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Email could not be sent.");
                Debug.WriteLine("Exception caught in RetryIfBusy(): {0}", ex.ToString());
                return StatusCode(500, "Internal server error");
            }

        }

    }
}