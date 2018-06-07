using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class AgentSafetySettingsModel
    {
        public string PK_ShowingSafetySettingsID { get; set; }
        public string AgentID { get; set; }
        public string SH_SafetyTimerStatue { get; set; }
        public string SH_EmergencyContact { get; set; }
        public string SH_EmergencyContactName { get; set; }
        public string SH_EmergencyContactImage { get; set; }
        public string SH_SafetyTimerLimit { get; set; }
        public string SH_TimerExtensionAgent { get; set; }
        public string SH_TimerAuthCode { get; set; }
        public string SH_EmergencyCode { get; set; }
        public string SH_Call_TimerExpire { get; set; }
        public string SH_TextEmergContact_TimerExpire { get; set; }
        public string SH_Message_TimerExpire { get; set; }
        public string SH_Call_CodeEntered { get; set; }
        public string SH_TextEmergContact_CodeEntered { get; set; }
        public string SH_Message_CodeEntered { get; set; }
        //For Open houses
        public string PK_OpenHouseSafetySettingID { get; set; }
        public string OH__SafetyTimerStatue { get; set; }
        public string OH_EmergencyContact { get; set; }
        public string OH_EmergencyContactName { get; set; }
        public string OH_EmergencyContactImage { get; set; }
        public string OH_SafetyTimerLimit { get; set; }
        public string OH_TimerExtensionAgent { get; set; }
        public string OH_TimerAuthCode { get; set; }
        public string OH_EmergencyCode { get; set; }
        public string OH_Call_TimerExpire { get; set; }
        public string OH_TextEmergContact_TimerExpire { get; set; }
        public string OH_Message_TimerExpire { get; set; }
        public string OH_Call_CodeEntered { get; set; }
        public string OH_TextEmergContact_CodeEntered { get; set; }
        public string OH_Message_CodeEntered { get; set; }
        
    }
}
