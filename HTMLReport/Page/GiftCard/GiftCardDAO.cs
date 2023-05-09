using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLReport.Page.GiftCard
{
    public sealed class GiftCardDAO
    {
        public string Amount { get; set; }
        public string CustomAmount { get; set; }
        public string DeliveryEmail { get; set; }
        public string From { get; set; }
        public string Message { get; set; }

        public string Quantity { get; set; }
        public string DeliveryDate { get; set; }
    }
}
