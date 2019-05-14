using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    
    /// <summary>
    /// <para>The RepositoryBase class implementing IRepositoryBase interface.</para>
    /// Contains all repository methods of EntityFramework.
    /// <remarks>
    /// <para>This class performs various actions on db using EntityFramework.</para>
    /// </remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class               //class having methods defined on db.
    {
        /// <value>Gets the value of the context.</value>
        /// <summary>
        /// Gets the value of the context.
        /// </summary>
        protected RepositoryContext RepositoryContext { get; set; }

        /// <summary>
        /// Constructor for RepositoryBase class.
        /// </summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }


        /// <summary>This function returns DbSet from db using the context.</summary>
        /// <returns>DbSet from db using the context.</returns>
        public IEnumerable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>();                     //return a DbSet from db.
        }

        
        /// <summary>This function returns a particular DbSet from DB.</summary>
        /// <param name="expression">
        /// A lambda expression defining the condition to search
        /// </param>
        /// <returns>Group object with particular id.</returns>
        /// <example>(group => group.id.Equals(groupId))</example>
        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression);       //return a DbSet from db using the lambda Expression as condition.
        }

        //public List<string> FindByColumn(Expression<Func<T, List<string>>> expression)
        //{
        //    return this.RepositoryContext.Set<T>().Select(expression);
        //}


        /// <summary>This function adds a DbSet in the db.</summary>
        /// <param name="entity">Class of type any.</param>
        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);                //creates to query upon DbSet and add in the db.
        }


        /// <summary>This function updates a DbSet in the db.</summary>
        /// <param name="entity">Class of type any.</param>
        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);             //creates to query upon DbSet and update in the db.
        }


        /// <summary>This function deletes a DbSet from the db.</summary>
        /// <param name="entity">Class of type any.</param>
        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);             //creates DbSet to query upon and update in the db.
        }


        /// <summary>This function saves a DbSet in the db.</summary>
        public void Save()
        {
            this.RepositoryContext.SaveChanges();                       //saves all the changes made in this context to db.
        }
    }
}