using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The TeamRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface ITeamRepository: IRepositoryBase<Team>
    {

        /// <summary>Get all teams objects.</summary>
        /// <returns>IEnumerable of tye team</returns>
        IEnumerable<Team> GetAllTeams();                            //this function will return all the teams in db.


        /// <summary>Gets the team by id.</summary>
        /// <param name="id">The id of the team to be searched.</param>
        /// <returns>team object with given id.</returns>
        Team GetTeamById(int id);                                   //this function will return a team with specified id.


        /// <summary>Gets the team by name.</summary>
        /// <param name="gName">Name of the team to be searched.</param>
        /// <returns>team object with given name.</returns>
        Team GetTeamByName(string gName);                           //this function will return a team with specified name.


        /// <summary>Creates the team.</summary>
        /// <param name="team">The team object.</param>
        void CreateTeam(Team team);                                //this function will create a new team entry in db.


        /// <summary>Updates the team.</summary>
        /// <param name="dbTeam">The database team.</param>
        /// <param name="team">The team object.</param>
        void UpdateTeam(Team dbTeam, Team team);                 //this function lets you update details of team and the admin.


        /// <summary>Deletes the team.</summary>
        /// <param name="team">The team object.</param>
        void DeleteTeam(Team team);                                //this lets you delete a particular team entry from database.

    }
}
