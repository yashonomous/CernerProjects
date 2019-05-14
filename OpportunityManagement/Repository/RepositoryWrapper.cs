using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;

namespace Repository
{
    /// <summary>
    /// The RepositoryWrapper class around our GroupRepository.
    /// Contains all repository methods of EntityFramework.
    /// <remarks>
    /// This class performs various actions on db using EntityFramework.
    /// </remarks>
    /// </summary>
    public class RepositoryWrapper : IRepositoryWrapper             //wrapper class defining contents of wrapper interface
    {
        private RepositoryContext _repoContext;
        private IUserRepository _user;
        private IOpportunityRepository _opportunity;
        private IUser_OpportunityRepository _user_opportunity;

        /// <summary>Gets the group object.</summary>
        /// <value>The GroupRepository object.</value>
        public IUserRepository User
        {
            get
            {
                if (_user == null)                             //if a null group object is passed.
                {
                    _user = new UserRepository(_repoContext);             //assigning a new GroupRepository object.
                }

                return _user;
            }
        }

        /// <summary>Gets the Opportunity object.</summary>
        /// <value>The OpportunityRepository object.</value>
        public IOpportunityRepository Opportunity
        {
            get
            {
                if (_opportunity == null)                             //if a null group object is passed.
                {
                    _opportunity = new OpportunityRepository(_repoContext);             //assigning a new GroupRepository object.
                }

                return _opportunity;
            }
        }

        /// <summary>Gets the Opportunity object.</summary>
        /// <value>The OpportunityRepository object.</value>
        public IUser_OpportunityRepository User_Opportunity
        {
            get
            {
                if (_user_opportunity == null)                             //if a null group object is passed.
                {
                    _user_opportunity = new User_OpportunityRepository(_repoContext);             //assigning a new GroupRepository object.
                }

                return _user_opportunity;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="RepositoryWrapper"/> class.</summary>
        /// <param name="repositoryContext">The repository context.</param>
        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;                           //assign defined context to our context
        }
    }
}
