using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    /// <summary>Associate model class with properties as specified in db.</summary>
    [Table("group_solution")]
    public class GroupSolution
    {

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the group.</value>
        [Key]
        public int gs_id{ get; set; }
        //public Guid id { get; set; }                        //declared as PK.
        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the solution.</value>
        [ForeignKey("SolutionId")]
        public int SolutionId { get; set; }

        /// <summary>Gets or sets the id(property name should be exactly same as the column name in db schema).</summary>
        /// <value>The id of the group.</value>
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
    }
}
