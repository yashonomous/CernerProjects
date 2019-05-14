using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    /// <summary>Team model class with properties as specified in db.</summary>
    [Table("team")]
    public class Team
    {
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the group.</value>
        [Key]
        public int TeamId { get; set; }    //declared as PK.

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the solution.</value>
        [ForeignKey("SolutionId")]
        public int SolutionId { get; set; }


        /// <summary>Gets or sets the name(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string TeamName { get; set; }                //name of the group.

        /// <summary>Gets or sets the description(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "Owner is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string TeamOwner { get; set; }         //owner of solution.

        /// <summary>Gets or sets the deleted property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "deleted is required")]
        [StringLength(60, ErrorMessage = "deleted can't be longer than 60 characters")]
        public string deleted { get; set; }
    }
}
