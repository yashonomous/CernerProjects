using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Contracts
{

    /// <summary>
    /// <para>The RepositoryBase interface.</para>
    /// <para>Its a generic repository that will serve us all the CRUD methods</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>                 //interface for actions using EntityFramework
    {
        /// <summary>
        /// This function returns DbSet from db using the context.
        /// </summary>
        /// <returns>
        /// <para>DbSet from db using the context.</para>
        /// </returns>
        IEnumerable<T> FindAll();                       //this function finds for all the groups in db.

        /// <summary>
        /// This function returns a particular DbSet from DB.
        /// </summary>
        /// <returns>
        /// <para>Group object with particular id.</para>
        /// </returns>
        /// <param name="expression">A lambda expression defining the condition to search (<example>group => group.id.Equals(groupId))</example>)</param>
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);          //this function finds for a particular group in db given condition as a lambda expression.

        /// <summary>
        /// This function adds a DbSet in the db.
        /// </summary>
        /// <param name="entity">Class of type any.</param>
        void Create(T entity);          //this will create a entry in db using EF.

        /// <summary>
        /// This function updates a DbSet in the db.
        /// </summary>
        /// <param name="entity">Class of type any.</param>
        void Update(T entity);          //this will update a entry in db using EF.

        /// <summary>
        /// This function deletes a DbSet from the db.
        /// </summary>
        /// <param name="entity">Class of type any.</param>
        void Delete(T entity);          //this will delete a entry in db using EF.

        /// <summary>
        /// This function saves a DbSet in the db.
        /// </summary>
        void Save();                    //this is to save the current state of db using EF.
    }
}
