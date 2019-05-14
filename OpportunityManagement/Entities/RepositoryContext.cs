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

        /// <summary>Gets or sets the Users.</summary>
        /// <value>The Users.</value>
        public DbSet<User> Users { get; set; }                    //interacting with Users object using DbSet.

        /// <summary>Gets or sets the Opportunity.</summary>
        /// <value>The Opportunity.</value>
        public DbSet<Opportunity> Opportunities { get; set; }                    //interacting with Opportunity object using DbSet.

        /// <summary>Gets or sets the User_Opportunity.</summary>
        /// <value>The User_Opportunity.</value>
        public DbSet<User_Opportunity> User_Opportunities { get; set; }                    //interacting with User_Opportunity object using DbSet.

    }
}
