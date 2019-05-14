using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("login")]
    public class User
    {
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the user.</value>
        [Key]
        public int user_id { get; set; }

        /// <summary>Gets or sets the name(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(60, ErrorMessage = "UserName can't be longer than 60 characters")]
        public string UserName { get; set; }                //name of the group.

        /// <summary>Gets or sets the password(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }         //a short description of the group.

        /// <summary>Gets or sets the Qualification(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The admin.</value>
        [Required(ErrorMessage = "Qualification details is required")]
        [StringLength(100, ErrorMessage = "Qualification cannot be loner then 100 characters")]
        public string Qualification { get; set; }               //email id of the admin

        /// <summary>Gets or sets the Experience property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "Experience is required")]
        [StringLength(60, ErrorMessage = "Experience can't be longer than 60 characters")]
        public int Experience { get; set; }

        /// <summary>Gets or sets the Role property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "Role is required")]
        [StringLength(60, ErrorMessage = "Role can't be longer than 60 characters")]
        public string Role { get; set; }

        /// <summary>Gets or sets the Skills property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "Skills is required")]
        [StringLength(60, ErrorMessage = "Skills can't be longer than 60 characters")]
        public string Skills { get; set; }
    }
}
