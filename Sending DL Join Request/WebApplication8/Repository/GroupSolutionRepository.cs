using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    /// <summary>
    /// <para>The main AssociateRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class GroupSolutionRepository : RepositoryBase<GroupSolution>, IGroupSolutionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupSolutionRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public GroupSolutionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the associates in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of Associate Objects.</para>
        /// </returns>
        public IEnumerable<GroupSolution> GetAllGroupSolutions()
        {
            return FindAll().OrderBy(gp => gp.GroupId);                    //using IRepositoryBase for interacting with db using EF.
                                                                               //returns the group objects sorted by Name.
        }


        /// <summary>Creates the group and solution.</summary>
        /// <param name="groupSolution">The groupSolution object.</param>
        public void CreateGroupSolution(GroupSolution groupSolution)
        {
            Create(groupSolution);                                     //create a group object entry in db.
            Save();
        }

        /// <summary>Gets all associates.</summary>
        /// <param name="groupId">The groupSolution object.</param>
        /// <returns>group solution object.</returns>
        public IEnumerable<GroupSolution> GetAllSolutionsByGroupId(int groupId)
        {
            return FindByCondition(group => group.GroupId.Equals(groupId));
        }
    }
}
