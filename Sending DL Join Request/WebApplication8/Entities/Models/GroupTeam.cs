using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    /// <summary>Group Team model class with properties as specified in db.</summary>
    [Table("group_team")]
    public class GroupTeam
    {

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the group team.</value>
        [Key]
        public int gt_id { get; set; }                  //declared as PK.
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the team.</value>
        [ForeignKey("TeamId")]
        public int TeamId { get; set; }

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the group.</value>
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
    }
}
