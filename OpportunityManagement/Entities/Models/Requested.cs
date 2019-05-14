using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Requested
    {
        
        public int user_opportunity_id { get; set; }

        public string User_Name { get; set; }

        public string Opportunity_Description { get; set; }

        public DateTime Start_Date { get; set; }

        public string Is_Accepted { get; set; }

        public string Color { get; set; }
    }
}
