using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("opportunity")]
    public class Opportunity
    {
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the user.</value>
        [Key]
        public int o_id { get; set; }

        /// <summary>Gets or sets the OpportunityDescription(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "OpportunityDescription is required")]
        [StringLength(200, ErrorMessage = "OpportunityDescription can't be longer than 60 characters")]
        public string OpportunityDescription { get; set; }                //name of the group.

        /// <summary>Gets or sets the StartTime(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "StartTime is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime { get; set; }         //a short description of the group.
        
        /// <summary>Gets or sets the EndTime(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The admin.</value>
        [Required(ErrorMessage = "EndTime is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime { get; set; }               //email id of the admin

        /// <summary>Gets or sets the isVacant(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The admin.</value>
        [Required(ErrorMessage = "IsVacant is required")]
        [StringLength(10, ErrorMessage = "IsVacant can't be longer than 60 characters")]
        public string IsVacant { get; set; }               //email id of the admin

        
        public string Color { get; set; }

        /// <summary>Gets or sets the Skills property(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The deleted sets to true or false.</value>
        [Required(ErrorMessage = "Skill is required")]
        [StringLength(20, ErrorMessage = "Skill can't be longer than 60 characters")]
        public string Skill { get; set; }
    }
}
