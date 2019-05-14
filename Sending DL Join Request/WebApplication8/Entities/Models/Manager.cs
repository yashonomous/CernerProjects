using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    /// <summary>Manager model class with properties as specified in db.</summary>
    [Table("manager")]
    public class Manager
    {
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the manager.</value>
        [Key]
        public int ManagerId { get; set; }    //declared as PK.

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the team.</value>
        [ForeignKey("TeamId")]
        public int TeamId { get; set; }


        /// <summary>Gets or sets the name(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string ManagerName { get; set; }                //name of the manager.

        /// <summary>Gets or sets the description(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "AssociateId is required")]
        public string AssociateId { get; set; }         //id of manager.

        /// <summary>Gets or sets the description(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; }         //mail id of manager.

        /// <summary>Gets or sets the deleted property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "deleted is required")]
        [StringLength(60, ErrorMessage = "deleted can't be longer than 60 characters")]
        public string deleted { get; set; }
    }
}
