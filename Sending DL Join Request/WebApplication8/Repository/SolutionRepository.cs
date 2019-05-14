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
    /// <para>The main SolutionRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class SolutionRepository: RepositoryBase<Solution>, ISolutionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public SolutionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the solutions in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of solution Objects.</para>
        /// </returns>
        public IEnumerable<Solution> GetAllSolutions()
        {
            //return FindAll().OrderBy(soln => soln.SolutionName);                    //using IRepositoryBase for interacting with db using EF.
            //returns the solution objects sorted by Name.
            return FindByCondition(group => group.deleted.Equals("false")).OrderBy(soln => soln.SolutionName);
        }

        /// <summary>
        /// This function returns all the solutions in DB.
        /// </summary>
        /// <returns>
        /// <para>solution object with particular id.</para>
        /// </returns>
        /// <param name="solutionId">An integer representing solution ID.</param>
        public Solution GetSolutionById(int solutionId)
        {
            return FindByCondition(solution => solution.SolutionId.Equals(solutionId)).DefaultIfEmpty(new Solution())
                    .FirstOrDefault();                          //returns the solution object with the given solutionId.
        }

        /// <summary>
        /// This function returns a particular solution by name.
        /// </summary>
        /// <returns>
        /// <para>solution object with particular name.</para>
        /// </returns>
        /// <param name="solutionName">A string representing name of the solution to be searched.</param>
        public Solution GetSolutionByName(string solutionName)
        {
            return FindByCondition(solution => solution.SolutionName.Equals(solutionName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();                          //returns the solution object with the given solutionName.    
        }

        /// <summary>
        /// This function creates a solution entry in DB.
        /// </summary>
        /// <remarks>
        /// This creates and saves a new solution entry in db.
        /// </remarks>
        /// <param name="solution">An object of type solution</param>
        public void CreateSolution(Solution solution)
        {
            Create(solution);                                     //create a solution object entry in db.
            Save();                                            //saves the state to db.
        }

        /// <summary>
        /// This function updates a solution entry in DB.
        /// </summary>
        /// <remarks>
        /// This updates and saves a new solution entry in db.
        /// </remarks>
        /// <param name="dbSolution">An solution object of type solution</param>
        /// <param name="solution">An object of type solution to be mapped to db</param>
        public void UpdateSolution(Solution dbSolution, Solution solution)
        {
            dbSolution.MapSolution(solution);                                //maps our solution object to one present in the db.
            Update(dbSolution);                                   //updates it.
            Save();                                            //save the updated state to db.
        }

        /// <summary>
        /// This function delete a solution entry in DB.
        /// </summary>
        /// <remarks>
        /// This delete and saves a new solution entry in db.
        /// </remarks>
        /// <param name="solution">An object of type solution</param>
        public void DeleteSolution(Solution solution)
        {
            Delete(solution);                                      //delete the given solution object.
            Save();
        }

    }
}
