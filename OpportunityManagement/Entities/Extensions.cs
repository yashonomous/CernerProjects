using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public static class Extensions
    {
        /// <summary>Maps the specified group.</summary>
        /// <param name="dbOpportunity">The database group.</param>
        /// <param name="opportunity">The group.</param>
        public static void MapOpportunity(this Opportunity dbOpportunity, Opportunity opportunity)
        {
            dbOpportunity.OpportunityDescription = opportunity.OpportunityDescription;
            dbOpportunity.StartTime = opportunity.StartTime;
            dbOpportunity.EndTime = opportunity.EndTime;
            dbOpportunity.IsVacant = opportunity.IsVacant;
        }

        /// <summary>Maps the specified group.</summary>
        /// <param name="dbUserOpportunity">The database group.</param>
        /// <param name="userOpportunity">The group.</param>
        public static void MapUserOpportunity(this User_Opportunity dbUserOpportunity, User_Opportunity userOpportunity)
        {
            dbUserOpportunity.user_opportunity_id = userOpportunity.user_opportunity_id;
            dbUserOpportunity.Opportunity_Id = userOpportunity.Opportunity_Id;
            dbUserOpportunity.User_Id = userOpportunity.User_Id;
            dbUserOpportunity.Is_Accepted = userOpportunity.Is_Accepted;
            dbUserOpportunity.Request_Date = userOpportunity.Request_Date;
        }
    }
}
