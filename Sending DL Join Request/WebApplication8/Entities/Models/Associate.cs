using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    /// <summary>Associate model class with properties as specified in db.</summary>
    [Table("associate")]
    public class Associate                  //class containing Group object.
    {
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the assoc entry.</value>
        [Key]
        public int assocId { get; set; }                    //declared as PK.


        /// <summary>Gets or sets the associate ID(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The Associate ID.</value>
        [Required(ErrorMessage = "Associate ID is required")]
        public string AssociateId { get; set; }                //name of the group.
    }
}

