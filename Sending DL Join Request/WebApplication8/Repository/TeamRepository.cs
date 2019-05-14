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
    class TeamRepository : RepositoryBase<Team>, ITeamRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public TeamRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the teams in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of team Objects.</para>
        /// </returns>
        public IEnumerable<Team> GetAllTeams()
        {
            // return FindAll().OrderBy(team => team.TeamName);                    //using IRepositoryBase for interacting with db using EF.
            //returns the solution objects sorted by Name.
            return FindByCondition(group => group.deleted.Equals("false")).OrderBy(team => team.TeamName);
        }

        /// <summary>
        /// This function returns all the teams in DB.
        /// </summary>
        /// <returns>
        /// <para>team object with particular id.</para>
        /// </returns>
        /// <param name="teamId">An integer representing team ID.</param>
        public Team GetTeamById(int teamId)
        {
            return FindByCondition(team => team.TeamId.Equals(teamId)).DefaultIfEmpty(new Team())
                    .FirstOrDefault();                          //returns the team object with the given teamId.
        }

        /// <summary>
        /// This function returns a particular team by name.
        /// </summary>
        /// <returns>
        /// <para>team object with particular name.</para>
        /// </returns>
        /// <param name="teamName">A string representing name of the team to be searched.</param>
        public Team GetTeamByName(string teamName)
        {
            return FindByCondition(team => team.TeamName.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();                          //returns the team object with the given teamName.    
        }

        /// <summary>
        /// This function creates a team entry in DB.
        /// </summary>
        /// <remarks>
        /// This creates and saves a new team entry in db.
        /// </remarks>
        /// <param name="team">An object of type team</param>
        public void CreateTeam(Team team)
        {
            Create(team);                                     //create a team object entry in db.
            Save();                                            //saves the state to db.
        }

        /// <summary>
        /// This function updates a team entry in DB.
        /// </summary>
        /// <remarks>
        /// This updates and saves a new team entry in db.
        /// </remarks>
        /// <param name="dbTeam">An team object of type team</param>
        /// <param name="team">An object of type team to be mapped to db</param>
        public void UpdateTeam(Team dbTeam, Team team)
        {
            dbTeam.MapTeam(team);                                //maps our team object to one present in the db.
            Update(dbTeam);                                   //updates it.
            Save();                                            //save the updated state to db.
        }

        /// <summary>
        /// This function delete a team entry in DB.
        /// </summary>
        /// <remarks>
        /// This delete and saves a new team entry in db.
        /// </remarks>
        /// <param name="team">An object of type team</param>
        public void DeleteTeam(Team team)
        {
            Delete(team);                                      //delete the given team object.
            Save();
        }
    }
}
