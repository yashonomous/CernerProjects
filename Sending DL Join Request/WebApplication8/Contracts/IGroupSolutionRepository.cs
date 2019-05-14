using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The GroupSolutionRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IGroupSolutionRepository : IRepositoryBase<GroupSolution>
    {
        /// <summary>Gets all solutions.</summary>
        /// <returns>IEnumerable of type solution.</returns>
        IEnumerable<GroupSolution> GetAllGroupSolutions();

        /// <summary>Creates the group and solution.</summary>
        /// <param name="groupSolution">The groupSolution object.</param>
        void CreateGroupSolution(GroupSolution groupSolution);                                //this function will create a new group entry in db.

        /// <summary>Gets all solutions.</summary>
        /// /// <param name="groupId">The groupSolution object.</param>
        /// <returns>group solution object.</returns>
        IEnumerable<GroupSolution> GetAllSolutionsByGroupId(int groupId);
    }
}
