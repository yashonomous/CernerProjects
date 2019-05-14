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
    /// <para>The main GroupRepository class.</para>
    /// Contains all methods for performing operations on DB.
    /// <remarks>
    /// This class performs various actions on db.
    /// </remarks>
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository              //class defining actions on Group Repository
    {

        /// <summary>Initializes a new instance of the <see cref="GroupRepository"/> class.</summary>
        /// <param name="repositoryContext">A RepositoryContext class object</param>
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            
        }

        /// <summary>
        /// This function returns all the groups in DB.
        /// </summary>
        /// <returns>
        /// <para>IEnumerable of Group Objects.</para>
        /// </returns>
        public IEnumerable<User> GetAllUsers()
        {
            //return FindAll().OrderBy(gp => gp.Name);                    //using IRepositoryBase for interacting with db using EF.
            //returns the group objects sorted by Name.
            return FindAll().OrderBy(user => user.UserName);
        }

        /// <summary>
        /// This function returns a particular group by id.
        /// </summary>
        /// <returns>
        /// <para>Group object with particular name.</para>
        /// </returns>
        /// <param name="userId">A string representing name of the group to be searched.</param>
        public User GetUserById(int userId)
        {
            return FindByCondition(user => user.user_id.Equals(userId)).FirstOrDefault();                          //returns the group object with the given groupName.    
        }

        /// <summary>
        /// This function returns a particular group by name.
        /// </summary>
        /// <returns>
        /// <para>Group object with particular name.</para>
        /// </returns>
        /// <param name="groupName">A string representing name of the group to be searched.</param>
        public User GetUserByName(string userName)
        {
            return FindByCondition(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefault();                          //returns the group object with the given groupName.    
        }

        /// <summary>Check if the associate exists.</summary>
        /// <param name="assocId">Associate Id.</param>
        /// <returns>boolean value</returns>
        public bool checkUserExists(string userName, string password)
        {
            var user = FindByCondition(usr => usr.UserName.Equals(userName) && usr.Password.Equals(password)).DefaultIfEmpty(new User())
                    .FirstOrDefault();
            if (user.UserName == null && user.user_id == 0 && user.Password == null)
            {
                return false;
            }
            return true;

        }
    }
}
