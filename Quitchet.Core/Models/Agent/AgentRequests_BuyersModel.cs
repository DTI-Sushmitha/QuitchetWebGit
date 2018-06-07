using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class AgentRequests_BuyersModel
    {
        public string PK_UserAgentsID { get; set; }
        public string PK_UserID { get; set; }
        public string FK_UserID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Result { get; set; }
        public string Status { get; set; }
        public string STATUS { get; set; }
        public string Note { get; set; }
        public string RequestsCount { get; set; }
    }
}
