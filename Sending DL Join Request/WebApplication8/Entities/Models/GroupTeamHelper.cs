using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    /// <summary>Class which has solution names string and group id.</summary>
    public class GroupTeamHelper
    {
        /// <summary>Gets or sets the recepient string.</summary>
        /// <value>The recepient string.</value>
        public int[] teamArray { get; set; }            //comma delimited string containing mail ids.

        /// <summary>Gets or sets the requested groups.</summary>
        /// <value>The requested groups.</value>
        public int groupId { get; set; }            //comma delimited string containing corresponding groups.
    }
}
