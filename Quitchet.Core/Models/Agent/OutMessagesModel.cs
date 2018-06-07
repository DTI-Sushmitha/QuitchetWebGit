using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class OutMessagesModel
    {
        public string ChatID { get; set; }
        public string From_IDs { get; set; }
        public string FROM_FRISTNAME { get; set; }
        public string FROM_LASTNAME { get; set; }
        public string To_IDs { get; set; }
        public string TO_FRISTNAME { get; set; }
        public string TO_LASTNAME { get; set; }
        public string Message { get; set; }
        public string Attachments { get; set; }
        public string View_Status { get; set; }
        public string OS_Type_ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Created_Date { get; set; }
        public string FROM_IMAGE { get; set; }
        public string TO_IMAGE { get; set; }
    }

    public class FormUserDetailsModel
    {
        public string PK_CHATID { get; set; }
        public string ChatType { get; set; }
        public string IsOpenChatMember { get; set; }
        public string PK_USERID { get; set; }
        public string NAME { get; set; }
        public string MobileNo { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }
        public string Email { get; set; }
        public string COUNT_OF_MSGS { get; set; }
        public string IMAGE { get; set; }
        public string MLSID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string AgentID { get; set; }
    }
}
