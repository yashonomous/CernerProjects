using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    /// <summary>Wrapper arount the Group repository class</summary>
    public interface IRepositoryWrapper         //wrapper interface for repository.
    {
        /// <summary>Gets the group object.</summary>
        /// <value>The GroupRepository object.</value>
        IGroupRepository Group { get; }

        /// <summary>Gets the associate object.</summary>
        /// <value>The AssociateRepository interface.</value>
        IAssociateRepository Associate { get; }

        /// <summary>Gets the solution object.</summary>
        /// <value>The SolutionRepository interface.</value>
        ISolutionRepository Solution { get; }

        /// <summary>Gets the team.</summary>
        /// <value>The TeamRepository interface.</value>
        ITeamRepository Team { get; }

        /// <summary>Gets the manager.</summary>
        /// <value>The ManagerRepository interface.</value>
        IManagerRepository Manager { get; }

        /// <summary>Gets the solution and group.</summary>
        /// <value>The GroupSolutionRepository interface.</value>
        IGroupSolutionRepository GroupSolution{ get; }

        /// <summary>Gets the solution and group.</summary>
        /// <value>The GroupTeamRepository interface.</value>
        IGroupTeamRepository GroupTeam { get; }
    }
}
