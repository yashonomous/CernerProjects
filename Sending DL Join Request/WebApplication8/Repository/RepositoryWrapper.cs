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
        private IGroupRepository _group;
        private IAssociateRepository _associate;
        private ISolutionRepository _solution;
        private ITeamRepository _team;
        private IManagerRepository _manager;
        private IGroupSolutionRepository _groupSolution;
        private IGroupTeamRepository _groupTeam;

        /// <summary>Gets the group object.</summary>
        /// <value>The GroupRepository object.</value>
        public IGroupRepository Group
        {
            get
            {
                if (_group == null)                             //if a null group object is passed.
                {
                    _group = new GroupRepository(_repoContext);             //assigning a new GroupRepository object.
                }

                return _group;
            }
        }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        public IAssociateRepository Associate
        {
            get
            {
                if(_associate == null)
                {
                    _associate = new AssociateRepository(_repoContext);
                }
                return _associate;
            }
        }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        public ISolutionRepository Solution
        {
            get
            {
                if (_solution == null)
                {
                    _solution = new SolutionRepository(_repoContext);
                }
                return _solution ;
            }
        }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        public ITeamRepository Team
        {
            get
            {
                if (_team == null)
                {
                    _team= new TeamRepository(_repoContext);
                }
                return _team;
            }
        }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        public IManagerRepository Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = new ManagerRepository(_repoContext);
                }
                return _manager;
            }
        }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        public IGroupSolutionRepository GroupSolution
        {
            get
            {
                if (_groupSolution == null)
                {
                    _groupSolution = new GroupSolutionRepository(_repoContext);
                }
                return _groupSolution;
            }
        }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        public IGroupTeamRepository GroupTeam
        {
            get
            {
                if (_groupTeam == null)
                {
                    _groupTeam = new GroupTeamRepository(_repoContext);
                }
                return _groupTeam;
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
