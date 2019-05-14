using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class Opp_Display
    {
        public int o_id { get; set; }

        public string Opportunity_Description { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }

        public string Is_Accepted { get; set; }
    }
}
