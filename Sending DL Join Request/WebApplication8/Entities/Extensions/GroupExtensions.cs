using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    /// <summary>Class for mapping given group object to db group object.</summary>
    public static class GroupExtensions
    {
        /// <summary>Maps the specified group.</summary>
        /// <param name="dbGroup">The database group.</param>
        /// <param name="group">The group.</param>
        public static void MapGroup(this Group dbGroup, Group group)
        {
            dbGroup.Name = group.Name;
            dbGroup.Description = group.Description;
            dbGroup.Admin = group.Admin;
            dbGroup.deleted = group.deleted;
        }

        /// <summary>Maps the specified solution.</summary>
        /// <param name="dbSolution">The database solution.</param>
        /// <param name="solution">The solution.</param>
        public static void MapSolution(this Solution dbSolution, Solution solution)
        {
            dbSolution.SolutionName = solution.SolutionName;
            dbSolution.SolutionOwner = solution.SolutionOwner;
            dbSolution.deleted = solution.deleted;
        }

        /// <summary>Maps the specified team.</summary>
        /// <param name="dbTeam">The database team.</param>
        /// <param name="team">The team.</param>
        public static void MapTeam(this Team dbTeam, Team team)
        {
            dbTeam.SolutionId = team.SolutionId;
            dbTeam.TeamName = team.TeamName;
            dbTeam.TeamOwner = team.TeamOwner;
            dbTeam.deleted = team.deleted;
        }

        /// <summary>Maps the specified manager.</summary>
        /// <param name="dbManager">The database manager.</param>
        /// <param name="manager">The manager.</param>
        public static void MapManager(this Manager dbManager, Manager manager)
        {
            dbManager.TeamId = manager.TeamId;
            dbManager.ManagerName = manager.ManagerName;
            dbManager.AssociateId = manager.AssociateId;
            dbManager.EmailId = manager.EmailId;
            dbManager.deleted = manager.deleted;
        }
    }
}
