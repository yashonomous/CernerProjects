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
    /// <para>The main OpportunityRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class OpportunityRepository: RepositoryBase<Opportunity>, IOpportunityRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public OpportunityRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        /// <summary>
        /// This function returns all the opportunitys in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of opportunity Objects.</para>
        /// </returns>
        public IEnumerable<Opportunity> GetAllOpportunities()
        {
            //return FindAll().OrderBy(gp => gp.Name);                    //using IRepositoryBase for interacting with db using EF.
            //returns the opportunity objects sorted by Name.
            return FindAll().OrderBy(opportunity => opportunity.OpportunityDescription);
        }

        /// <summary>
        /// This function returns all the groups in DB.
        /// </summary>
        /// <returns>
        /// <para>Group object with particular id.</para>
        /// </returns>
        /// <param name="o_id">An integer representing group ID.</param>
        public Opportunity GetOpportunityById(int o_id)
        {
            return FindByCondition(opportunity => opportunity.o_id.Equals(o_id)).DefaultIfEmpty(new Opportunity())
                    .FirstOrDefault();                          //returns the group object with the given groupId.
        }


        /// <summary>
        /// This function creates a opportunity entry in DB.
        /// </summary>
        /// <remarks>
        /// This creates and saves a new opportunity entry in db.
        /// </remarks>
        /// <param name="opportunity">An object of type opportunity</param>
        public void CreateOpportunity(Opportunity opportunity)
        {
            Create(opportunity);                                     //create a opportunity object entry in db.
            Save();                                            //saves the state to db.
        }

        /// <summary>
        /// This function updates a opportunity entry in DB.
        /// </summary>
        /// <remarks>
        /// This updates and saves a new opportunity entry in db.
        /// </remarks>
        /// <param name="dbOpportunity">An opportunity object of type opportunity</param>
        /// <param name="opportunity">An object of type opportunity to be mapped to db</param>
        public void UpdateOpportunity(Opportunity dbOpportunity, Opportunity opportunity)
        {
            dbOpportunity.MapOpportunity(opportunity);                                //maps our opportunity object to one present in the db.
            Update(dbOpportunity);                                   //updates it.
            Save();                                            //save the updated state to db.
        }

        /// <summary>
        /// This function delete a opportunity entry in DB.
        /// </summary>
        /// <remarks>
        /// This delete and saves a new opportunity entry in db.
        /// </remarks>
        /// <param name="opportunity">An object of type opportunity</param>
        public void DeleteOpportunity(Opportunity opportunity)
        {
            Delete(opportunity);                                      //delete the given opportunity object.
            Save();
        }
    }
}
