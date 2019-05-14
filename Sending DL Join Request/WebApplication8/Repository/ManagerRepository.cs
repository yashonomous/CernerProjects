
using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    /// <summary>
    /// <para>The main ManagerRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class ManagerRepository: RepositoryBase<Manager>, IManagerRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public ManagerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the managers in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of manager Objects.</para>
        /// </returns>
        public IEnumerable<Manager> GetAllManagers()
        {
            //return FindAll().OrderBy(manager => manager.ManagerName);                    //using IRepositoryBase for interacting with db using EF.
            //returns the solution objects sorted by Name.
            return FindByCondition(group => group.deleted.Equals("false")).OrderBy(manager => manager.ManagerName);
        }

        /// <summary>C:\Users\ys067342\source\repos\WebApplication8\Entities\Models\GroupSolution.cs
        /// This function returns all the managers in DB.
        /// </summary>
        /// <returns>
        /// <para>manager object with particular id.</para>
        /// </returns>
        /// <param name="managerId">An integer representing manager ID.</param>
        public Manager GetManagerById(int managerId)
        {
            return FindByCondition(manager => manager.ManagerId.Equals(managerId)).DefaultIfEmpty(new Manager())
                    .FirstOrDefault();                          //returns the manager object with the given managerId.
        }

        /// <summary>
        /// This function returns a particular manager by name.
        /// </summary>
        /// <returns>
        /// <para>manager object with particular name.</para>
        /// </returns>
        /// <param name="managerName">A string representing name of the manager to be searched.</param>
        public Manager GetManagerByName(string managerName)
        {
            return FindByCondition(manager => manager.ManagerName.Equals(managerName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();                          //returns the manager object with the given managerName.    
        }

        /// <summary>
        /// This function creates a manager entry in DB.
        /// </summary>
        /// <remarks>
        /// This creates and saves a new manager entry in db.
        /// </remarks>
        /// <param name="manager">An object of type manager</param>
        public void CreateManager(Manager manager)
        {
            Create(manager);                                     //create a manager object entry in db.
            Save();                                            //saves the state to db.
        }

        /// <summary>
        /// This function updates a manager entry in DB.
        /// </summary>
        /// <remarks>
        /// This updates and saves a new manager entry in db.
        /// </remarks>
        /// <param name="dbManager">An manager object of type manager</param>
        /// <param name="manager">An object of type manager to be mapped to db</param>
        public void UpdateManager(Manager dbManager, Manager manager)
        {
            dbManager.MapManager(manager);                                //maps our manager object to one present in the db.
            Update(dbManager);                                   //updates it.
            Save();                                            //save the updated state to db.
        }

        /// <summary>
        /// This function delete a manager entry in DB.
        /// </summary>
        /// <remarks>
        /// This delete and saves a new manager entry in db.
        /// </remarks>
        /// <param name="manager">An object of type manager</param>
        public void DeleteManager(Manager manager)
        {
            Delete(manager);                                      //delete the given manager object.
            Save();
        }
    }
}
