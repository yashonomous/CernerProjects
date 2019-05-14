using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("user_opportunity")]
    public class User_Opportunity
    {

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the user.</value>
        [Key]
        public int user_opportunity_id { get; set; }

        /// <summary>Gets or sets the OpportunityDescription(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "User_Id is required")]
        public int User_Id { get; set; }                //name of the group.

        /// <summary>Gets or sets the StartTime(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "Opportunity_Id is required")]
        public int Opportunity_Id { get; set; }         //a short description of the group.

        /// <summary>Gets or sets the IsAccepted(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The accepted status.</value>
        [Required(ErrorMessage = "Is_Accepted is required")]
        public string Is_Accepted { get; set; }         //a short description of the group.

        /// <summary>Gets or sets the IsAccepted(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The accepted status.</value>
        [Required(ErrorMessage = "Request_Date is required")]
        public DateTime Request_Date { get; set; }         //a short description of the group.

    }
}
