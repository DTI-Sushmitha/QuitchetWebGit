using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class NotificationsModel
    {
        public string PK_NotificationID { get; set; }
        public string Notification { get; set; }
        public string Action_OnClick { get; set; }
        public string ViewStatus { get; set; }
        public string Icon { get; set; }
        public string ReferanceID { get; set; }
    }
}
