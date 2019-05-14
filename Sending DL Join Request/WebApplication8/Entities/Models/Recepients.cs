using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    /// <summary>Class which has recipients string and respective groups string.</summary>
    public class Recepients                         //class having recipient info for sending mail
    {
        /// <summary>Gets or sets the recepient string.</summary>
        /// <value>The recepient string.</value>
        public string recepientString { get; set; }            //comma delimited string containing mail ids.
        
        /// <summary>Gets or sets the requested groups.</summary>
        /// <value>The requested groups.</value>
        public string requestedGroups { get; set; }            //comma delimited string containing corresponding groups.
    }
}
