using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The SolutionRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface ISolutionRepository : IRepositoryBase<Solution>
    {
        /// <summary>Get all solutions objects.</summary>
        /// <returns>IEnumerable of tye solution</returns>
        IEnumerable<Solution> GetAllSolutions();                            //this function will return all the solutions in db.


        /// <summary>Gets the solution by id.</summary>
        /// <param name="id">The id of the solution to be searched.</param>
        /// <returns>solution object with given id.</returns>
        Solution GetSolutionById(int id);                                   //this function will return a solution with specified id.


        /// <summary>Gets the solution by name.</summary>
        /// <param name="gName">Name of the solution to be searched.</param>
        /// <returns>solution object with given name.</returns>
        Solution GetSolutionByName(string gName);                           //this function will return a solution with specified name.


        /// <summary>Creates the solution.</summary>
        /// <param name="solution">The solution object.</param>
        void CreateSolution(Solution solution);                                //this function will create a new solution entry in db.


        /// <summary>Updates the solution.</summary>
        /// <param name="dbSolution">The database solution.</param>
        /// <param name="solution">The solution object.</param>
        void UpdateSolution(Solution dbSolution, Solution solution);                 //this function lets you update details of solution and the admin.


        /// <summary>Deletes the solution.</summary>
        /// <param name="solution">The solution object.</param>
        void DeleteSolution(Solution solution);                                //this lets you delete a particular solution entry from database.

    }
}
