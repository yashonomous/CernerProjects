using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    /// <summary>Group model class with properties as specified in db.</summary>
    [Table("dl_groups")]
    public class Group                  //class containing Group object.
    {
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the group.</value>
        [Key]
        public int id { get; set; }
        //public Guid id { get; set; }                        //declared as PK.


        /// <summary>Gets or sets the name(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }                //name of the group.

        /// <summary>Gets or sets the description(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }         //a short description of the group.

        /// <summary>Gets or sets the admin(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The admin.</value>
        [Required(ErrorMessage = "Admin details is required")]
        [StringLength(100, ErrorMessage = "Admin cannot be loner then 100 characters")]
        public string Admin { get; set; }               //email id of the admin

        /// <summary>Gets or sets the deleted property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "deleted is required")]
        [StringLength(60, ErrorMessage = "deleted can't be longer than 60 characters")]
        public string deleted { get; set; }                //name of the group.
    }
}

