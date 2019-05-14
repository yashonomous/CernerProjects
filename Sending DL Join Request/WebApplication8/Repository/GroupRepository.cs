using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;

namespace Repository
{
    /// <summary>
    /// <para>The main GroupRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository              //class defining actions on Group Repository
    {

        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public GroupRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the groups in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of Group Objects.</para>
        /// </returns>
        public IEnumerable<Group> GetAllGroups()
        {
            //return FindAll().OrderBy(gp => gp.Name);                    //using IRepositoryBase for interacting with db using EF.
            //returns the group objects sorted by Name.
            return FindByCondition(group => group.deleted.Equals("false")).OrderBy(gp => gp.Name);
        }

        /// <summary>
        /// This function returns all the groups in DB.
        /// </summary>
        /// <returns>
        /// <para>Group object with particular id.</para>
        /// </returns>
        /// <param name="groupId">An integer representing group ID.</param>
        public Group GetGroupById(int groupId)
        {
            return FindByCondition(group => group.id.Equals(groupId)).DefaultIfEmpty(new Group())
                    .FirstOrDefault();                          //returns the group object with the given groupId.
        }

        /// <summary>
        /// This function returns a particular group by name.
        /// </summary>
        /// <returns>
        /// <para>Group object with particular name.</para>
        /// </returns>
        /// <param name="groupName">A string representing name of the group to be searched.</param>
        public Group GetGroupByName(string groupName)
        {
            return FindByCondition(group => group.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();                          //returns the group object with the given groupName.    
        }

        /// <summary>
        /// This function creates a group entry in DB.
        /// </summary>
        /// <remarks>
        /// This creates and saves a new group entry in db.
        /// </remarks>
        /// <param name="group">An object of type Group</param>
        public void CreateGroup(Group group)
        {
            Create(group);                                     //create a group object entry in db.
            Save();                                            //saves the state to db.
        }

        /// <summary>
        /// This function updates a group entry in DB.
        /// </summary>
        /// <remarks>
        /// This updates and saves a new group entry in db.
        /// </remarks>
        /// <param name="dbGroup">An group object of type Group</param>
        /// <param name="group">An object of type Group to be mapped to db</param>
        public void UpdateGroup(Group dbGroup, Group group)
        {
            dbGroup.MapGroup(group);                                //maps our group object to one present in the db.
            Update(dbGroup);                                   //updates it.
            Save();                                            //save the updated state to db.
        }

        /// <summary>
        /// This function delete a group entry in DB.
        /// </summary>
        /// <remarks>
        /// This delete and saves a new group entry in db.
        /// </remarks>
        /// <param name="group">An object of type Group</param>
        public void DeleteGroup(Group group)
        {
            Delete(group);                                      //delete the given group object.
            Save();
        }
    }
}
