using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The GroupTeam interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IGroupTeamRepository : IRepositoryBase<GroupTeam>
    {
        /// <summary>Gets all teams.</summary>
        /// <returns>IEnumerable of type GroupSolution.</returns>
        IEnumerable<GroupTeam> GetAllGroupTeams();

        /// <summary>Creates the group and team.</summary>
        /// <param name="groupTeam">The groupSolution object.</param>
        void CreateGroupTeam(GroupTeam groupTeam);                                //this function will create a new group entry in db.

        /// <summary>Gets all teams.</summary>
        /// /// <param name="groupId">The groupSolution object.</param>
        /// <returns>group solution object.</returns>
        IEnumerable<GroupTeam> GetAllTeamsByGroupId(int groupId);
    }
}
