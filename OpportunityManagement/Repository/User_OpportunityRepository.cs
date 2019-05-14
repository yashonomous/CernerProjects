using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    /// <summary>
    /// <para>The main User_OpportunityRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class User_OpportunityRepository: RepositoryBase<User_Opportunity>, IUser_OpportunityRepository
    {

        /// <summary>Initializes a new instance of the <see cref="User_OpportunityRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public User_OpportunityRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        /// <summary>
        /// This function returns all the User_Opportunities in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of User_Opportunity Objects.</para>
        /// </returns>
        public IEnumerable<User_Opportunity> GetAllUserOpportunities()
        {
            return FindAll().OrderBy(user => user.User_Id);                    //using IRepositoryBase for interacting with db using EF.
                                                                           //returns the group objects sorted by Name.
        }
        
        /// <summary>
        /// This function returns all the User_Opportunities in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of User_Opportunity Objects.</para>
        /// </returns>
        /// /// <param name="id">An integer representing group ID.</param>
        public User_Opportunity GetUserOpportunityById(int id)
        {
            return FindByCondition(user_opportunity => user_opportunity.user_opportunity_id.Equals(id)).DefaultIfEmpty(new User_Opportunity())
                    .FirstOrDefault();                     //using IRepositoryBase for interacting with db using EF.
                                                           //returns the group objects sorted by Name.
        }

        /// <summary>Creates the user and opportunity.</summary>
        /// <param name="userOpportunity">The userOpportunity object.</param>
        public void CreateUserOpportunity(User_Opportunity userOpportunity)
        {
            Create(userOpportunity);                                     //create a group object entry in db.
            Save();
        }

        /// <summary>Gets all associates.</summary>
        /// <param name="groupId">The groupSolution object.</param>
        /// <returns>group solution object.</returns>
        public IEnumerable<User_Opportunity> GetAllOpportunitiesByUserId(int userId)
        {
            return FindByCondition(user => user.User_Id.Equals(userId));
        }

        /// <summary>
        /// This function updates a opportunity entry in DB.
        /// </summary>
        /// <remarks>
        /// This updates and saves a new opportunity entry in db.
        /// </remarks>
        /// <param name="dbOpportunity">An opportunity object of type opportunity</param>
        /// <param name="opportunity">An object of type opportunity to be mapped to db</param>
        public void UpdateUserOpportunity(User_Opportunity dbUserOpportunity, User_Opportunity userOpportunity)
        {
            dbUserOpportunity.MapUserOpportunity(userOpportunity);                                //maps our opportunity object to one present in the db.
            Update(dbUserOpportunity);                                   //updates it.
            Save();                                            //save the updated state to db.
        }
    }
}
