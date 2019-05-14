using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The ManagerRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IManagerRepository : IRepositoryBase<Manager>
    {

        /// <summary>Get all managers objects.</summary>
        /// <returns>IEnumerable of tye manager</returns>
        IEnumerable<Manager> GetAllManagers();                            //this function will return all the managers in db.


        /// <summary>Gets the manager by id.</summary>
        /// <param name="id">The id of the manager to be searched.</param>
        /// <returns>manager object with given id.</returns>
        Manager GetManagerById(int id);                                   //this function will return a manager with specified id.


        /// <summary>Gets the manager by name.</summary>
        /// <param name="gName">Name of the manager to be searched.</param>
        /// <returns>manager object with given name.</returns>
        Manager GetManagerByName(string gName);                           //this function will return a manager with specified name.


        /// <summary>Creates the manager.</summary>
        /// <param name="manager">The manager object.</param>
        void CreateManager(Manager manager);                                //this function will create a new manager entry in db.


        /// <summary>Updates the manager.</summary>
        /// <param name="dbManager">The database manager.</param>
        /// <param name="manager">The manager object.</param>
        void UpdateManager(Manager dbManager, Manager manager);                 //this function lets you update details of manager and the admin.


        /// <summary>Deletes the manager.</summary>
        /// <param name="manager">The manager object.</param>
        void DeleteManager(Manager manager);                                //this lets you delete a particular manager entry from database.

    }
}
