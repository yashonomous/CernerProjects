using System;
using System.Collections.Generic;
using System.Text;

using Entities.Models;


namespace Contracts
{
    /// <summary>
    /// <para>The GroupRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IGroupRepository : IRepositoryBase<Group>              //interface for Group Repository(having all actions on repository)
    {
        /// <summary>Get all groups objects.</summary>
        /// <returns>IEnumerable of tye Group</returns>
        IEnumerable<Group> GetAllGroups();                            //this function will return all the groups in db.


        /// <summary>Gets the group by id.</summary>
        /// <param name="id">The id of the group to be searched.</param>
        /// <returns>Group object with given id.</returns>
        Group GetGroupById(int id);                                   //this function will return a group with specified id.


        /// <summary>Gets the group by name.</summary>
        /// <param name="gName">Name of the group to be searched.</param>
        /// <returns>Group object with given name.</returns>
        Group GetGroupByName(string gName);                           //this function will return a group with specified name.


        /// <summary>Creates the group.</summary>
        /// <param name="group">The group object.</param>
        void CreateGroup(Group group);                                //this function will create a new group entry in db.


        /// <summary>Updates the group.</summary>
        /// <param name="dbGroup">The database group.</param>
        /// <param name="group">The group object.</param>
        void UpdateGroup(Group dbGroup, Group group);                 //this function lets you update details of group and the admin.


        /// <summary>Deletes the group.</summary>
        /// <param name="group">The group object.</param>
        void DeleteGroup(Group group);                                //this lets you delete a particular group entry from database.
    }
}
