using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    /// <summary>
    /// <para>Class acting as a middleware component for the communication with the database</para>
    /// It has DbSet properties which contain the table data from the database.
    /// </summary>
    public class RepositoryContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="RepositoryContext"/> class.</summary>
        /// <param name="options">The options for this context.</param>
        public RepositoryContext(DbContextOptions options)              //mapping our repository context with DbContext of EntityFramework
            : base(options)
        {
        }

        /// <summary>Gets or sets the groups.</summary>
        /// <value>The groups.</value>
        public DbSet<Group> Groups { get; set; }                    //interacting with group object using DbSet.

        /// <summary>Gets or sets the associate.</summary>
        /// <value>The associate.</value>
        public DbSet<Associate> Associate { get; set; }            //interacting with associate object using DbSet.

        /// <summary>Gets or sets the solution.</summary>
        /// <value>The solution.</value>
        public DbSet<Solution> Solutions { get; set; }            //interacting with solution object using DbSet.

        /// <summary>Gets or sets the team.</summary>
        /// <value>The team.</value>
        public DbSet<Team> Teams { get; set; }            //interacting with team object using DbSet.

        /// <summary>Gets or sets the manager.</summary>
        /// <value>The manager.</value>
        public DbSet<Manager> Managers { get; set; }            //interacting with manager object using DbSet.

        /// <summary>Gets or sets the manager.</summary>
        /// <value>The manager.</value>
        public DbSet<GroupSolution> GroupSolution { get; set; }            //interacting with manager object using DbSet.

        /// <summary>Gets or sets the manager.</summary>
        /// <value>The manager.</value>
        public DbSet<GroupTeam> GroupTeam { get; set; }            //interacting with manager object using DbSet.
    }
}
