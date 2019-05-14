using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>
    /// <para>The AssociateRepository interface.</para>
    /// <para>It contains methods which define all the HTTP request.</para>
    /// </summary>
    public interface IAssociateRepository : IRepositoryBase<Associate>
    {
        /// <summary>Check if the associate exists.</summary>
        /// <param name="assocId">Associate Id.</param>
        /// <returns>boolean value</returns>
        bool checkAssociateExists(string assocId);

        /// <summary>Gets all associates.</summary>
        /// <returns>IEnumerable of type Associate.</returns>
        IEnumerable<Associate> GetAllAssociates();


    }
}
