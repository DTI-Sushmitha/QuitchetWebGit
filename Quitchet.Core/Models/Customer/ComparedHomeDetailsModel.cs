using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class ComparedHomeDetailsModel
    {
        public string MlsidPhotoPath { get; set; }
        public int PK_CompareHomeID { get; set; }
        public string PhotoPath { get; set; }

        public string Address { get; set; }
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string SALE_RENT { get; set; }
        public string HOMEADDRESS { get; set; }
        public string HOMESTUFF { get; set; }
        public decimal PRICE { get; set; }

        // Save Changes
        public bool MAINVIEW { get; set; }
        public bool MASTERBEDROOM { get; set; }
        public bool MASTERBATHROOM { get; set; }
        public bool KITCHEN { get; set; }
        public bool Living_Great_Room { get; set; }
        public bool BACKVIEW { get; set; }
        public bool Other_Interior { get; set; }
        public bool OTHER { get; set; }
        public string ORDER1 { get; set; }
        public string ORDER2 { get; set; }
        public string ORDER3 { get; set; }
        public string ORDER4 { get; set; }
        public string ORDER5 { get; set; }
        public string ORDER6 { get; set; }
        public string ORDER7 { get; set; }
        public string ORDER8 { get; set; }

    }
}
