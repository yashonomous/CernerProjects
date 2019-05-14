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
    /// <para>The main AssociateRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class AssociateRepository : RepositoryBase<Associate>, IAssociateRepository
    {
        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public AssociateRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        /// <summary>
        /// This function returns all the associates in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of Associate Objects.</para>
        /// </returns>
        public IEnumerable<Associate> GetAllAssociates()
        {
            return FindAll().OrderBy(gp => gp.AssociateId);                    //using IRepositoryBase for interacting with db using EF.
                                                                        //returns the group objects sorted by Name.
        }

        /// <summary>Check if the associate exists.</summary>
        /// <param name="assocId">Associate Id.</param>
        /// <returns>boolean value</returns>
        public bool checkAssociateExists(string assocId)
        {
            var associate = FindByCondition(assoc => assoc.AssociateId.Equals(assocId)).DefaultIfEmpty(new Associate())
                    .FirstOrDefault();
            if (associate.AssociateId == null && associate.assocId == 0)
            {
                return false;
            }
            return true;

        }
    }
}
