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
    public class GroupTeamRepository : RepositoryBase<GroupTeam>, IGroupTeamRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupTeamRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public GroupTeamRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the associates in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of Associate Objects.</para>
        /// </returns>
        public IEnumerable<GroupTeam> GetAllGroupTeams()
        {
            return FindAll().OrderBy(gp => gp.GroupId);                    //using IRepositoryBase for interacting with db using EF.
                                                                           //returns the group objects sorted by Name.
        }


        /// <summary>Creates the group and Team.</summary>
        /// <param name="groupTeam">The groupTeam object.</param>
        public void CreateGroupTeam(GroupTeam groupTeam)
        {
            Create(groupTeam);                                     //create a group object entry in db.
            Save();
        }

        /// <summary>Gets all associates.</summary>
        /// <param name="groupId">The groupTeam object.</param>
        /// <returns>group Team object.</returns>
        public IEnumerable<GroupTeam> GetAllTeamsByGroupId(int groupId)
        {
            return FindByCondition(group => group.GroupId.Equals(groupId));
        }
    }
}
